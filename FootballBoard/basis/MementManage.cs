using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballBoard
{
    /// <summary>
    /// TextBoxの思い出具象クラス
    /// </summary>
    public sealed class TextBoxMemento : Memento<List<ObjectBase>,DataModel>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mementoData">思い出データ</param>
        /// <param name="txtBox">データ反映対象オブジェクト</param>
        public TextBoxMemento(List<ObjectBase> mementoData,DataModel target)
        {
            base.MementoData = mementoData;
            base.Target = target;
        }

        /// <summary>
        /// ターゲットに思い出を反映させます。
        /// </summary>
        public override void SetMemento(List<ObjectBase> mementoData)
        {
            base.MementoData = mementoData;

            //deepコピーする
            base.Target.ObjectList.Clear();
            for(int i = 0;i < mementoData.Count;i++)
            {
                ObjectBase b = DeepCopyHelper.DeepCopy<ObjectBase>(mementoData[i]);
                base.Target.ObjectList.Add(b);
            }
        }
    }
    //UNDO、REDOを管理

    /// <summary>
    /// CommandManager
    /// </summary>
    public sealed class CommandManager
    {
        private int _maxStack = int.MaxValue;
        private Stack<ICommand> _undoStack;
        private Stack<ICommand> _redoStack;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CommandManager()
        {
            _undoStack = new Stack<ICommand>();
            _redoStack = new Stack<ICommand>();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="max">最大保存数</param>
        public CommandManager(int maxStack) : this()
        {
            _maxStack = maxStack;
        }

        /// <summary>
        /// 呼び出し
        /// </summary>
        /// <param name="command">コマンド</param>
        public bool Invoke(ICommand command)
        {
            if (_undoStack.Count >= _maxStack) return false;
            command.Invoke();
            _redoStack.Clear();
            _undoStack.Push(command);
            return true;
        }

        /// <summary>
        /// 元に戻す
        /// </summary>
        public void Undo()
        {
            if (_undoStack.Count == 0) return;
            var command = _undoStack.Pop();
            command.Undo();
            _redoStack.Push(command);
        }

        /// <summary>
        /// やり直し
        /// </summary>
        public void Redo()
        {
            if (_redoStack.Count == 0) return;
            var command = _redoStack.Pop();
            command.Redo();
            _undoStack.Push(command);
        }

        /// <summary>
        /// リフレッシュ
        /// </summary>
        public void Refresh()
        {
            _undoStack.Clear();
            _redoStack.Clear();
        }
    }

    /// <summary>
    /// 思い出更新コマンド
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class MementoCommand<T1,T2> : ICommand
    {
        private Memento<T1,T2> _memento;
        private T1 _prev;
        private T1 _next;

        public MementoCommand(Memento<T1,T2> prev, Memento<T1,T2> next)
        {
            _memento = prev;
            _prev = prev.MementoData;
            _next = next.MementoData;
        }

        #region ICommand メンバ

        /// <summary>
        /// 呼び出し
        /// </summary>
        void ICommand.Invoke()
        {
            _prev = _memento.MementoData;
            _memento.SetMemento(_next);
        }

        /// <summary>
        /// 元に戻す
        /// </summary>
        void ICommand.Undo()
        {
            _memento.SetMemento(_prev);
        }

        /// <summary>
        /// やり直し
        /// </summary>
        void ICommand.Redo()
        {
            _memento.SetMemento(_next);
        }

        #endregion
    }
    /// <summary>
         /// 記念品(笑)抽象クラス
         /// </summary>
         /// <typeparam name="T1">思い出データの型</typeparam>
    public abstract class Memento<T1,T2>
    {
        /// <summary>
        /// 思い出データを取得または設定します。
        /// </summary>
        public T1 MementoData { get; protected set; }

        /// <summary>
        /// データ反映対象オブジェクトを取得または設定します。
        /// </summary>
        protected T2 Target { get; set; }

        /// <summary>
        /// 思い出を反映させます。
        /// </summary>
        public abstract void SetMemento(T1 _mementoData);
    }
    /// <summary>
    /// ICommandインターフェイス
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// 呼び出し
        /// </summary>
        void Invoke();
        /// <summary>
        /// 元に戻す
        /// </summary>
        void Undo();
        /// <summary>
        /// やり直し
        /// </summary>
        void Redo();
    }
}
