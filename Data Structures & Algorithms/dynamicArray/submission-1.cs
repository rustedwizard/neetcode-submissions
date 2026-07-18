public class DynamicArray {

    internal class DANode
    {
        internal int[] Value;
        internal DANode Next;
        
        internal DANode(int capacity)
        {
            this.Value = new int[capacity];
        }
    }

    private DANode _headNode;
    private int _nodeCount;
    private int _currentSize;
    private int _capacity;
    
    public DynamicArray(int capacity) {
        this._headNode = new DANode(capacity);
        this._currentSize = 0;
        this._capacity = capacity;
        this._nodeCount = 1;
    }

    public int Get(int i) {
        var (nodeIndex, elementIndex) = GetPosition(i);
        var current = _headNode;
        for (int j = 0; j < nodeIndex; j++)
        {
            current = current.Next;
        }
        return current.Value[elementIndex];
    }

    public void Set(int i, int n) {
        var (nodeIndex, elementIndex) = GetPosition(i);
        var current = _headNode;
        for (int j = 0; j < nodeIndex; j++)
        {
            current = current.Next;
        }
        current.Value[elementIndex] = n;
    }

    public void PushBack(int n) {
        if(this._capacity * this._nodeCount <= this._currentSize)
        {
            this.Resize();
        }
        this.Set(this._currentSize, n);
        this._currentSize++;
    }

    public int PopBack() {
        var result = this.Get(this._currentSize-1);
        this._currentSize--;
        return result;
    }

    private void Resize() {
        var tail = new DANode(this._capacity);
        var current = this._headNode;
        var tCurrent = tail;
        var temp = 1;
        while(current.Next != null)
        {
            current = current.Next;
            tCurrent.Next = new DANode(this._capacity);
            tCurrent = tCurrent.Next;
            temp++;
        }
        current.Next = tail;
        this._nodeCount += temp;
    }

    public int GetSize() {
        return this._currentSize;
    }

    public int GetCapacity() {
        return this._capacity * this._nodeCount;
    }

    private (int, int) GetPosition(int pos)
    {
        if (pos < this._capacity)
        {
            return (0, pos);
        }
        var first = Math.Floor((double)pos / this._capacity);
        var second = pos % this._capacity;
        return ((int)first, (int)second);
    }
}
