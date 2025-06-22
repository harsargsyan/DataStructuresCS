namespace DataStructuresCS.BinaryTree;

public class MinHeap<T> where T : IComparable<T>
{
    private readonly List<T> _heap;

    public MinHeap()
    {
        _heap = [];
    }

    public MinHeap(IEnumerable<T> collection)
    {
        _heap = new List<T>(collection);
        BuildHeap();
    }
    private bool IsEmpty => _heap.Count == 0;

    // Insert element into heap
    public void Insert(T item)
    {
        _heap.Add(item);
        HeapifyUp(_heap.Count - 1);
    }

    // Remove and return minimum element
    public T ExtractMin()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Heap is empty");

        T min = _heap[0];
        
        // Replace root with last element
        _heap[0] = _heap[_heap.Count - 1];
        _heap.RemoveAt(_heap.Count - 1);

        // Restore heap property
        if (!IsEmpty)
            HeapifyDown(0);

        return min;
    }

    // Peek at minimum element without removing
    public T GetMin()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Heap is empty");
        
        return _heap[0];
    }

    // Build heap from existing array (heapify)
    private void BuildHeap()
    {
        // Start from last non-leaf node and heapify down
        for (int i = (_heap.Count / 2) - 1; i >= 0; i--)
        {
            HeapifyDown(i);
        }
    }

    // Bubble element up to maintain min-heap property
    private void HeapifyUp(int index)
    {
        if (index == 0) return; // Root reached

        int parentIndex = GetParentIndex(index);
        
        // If current element is smaller than parent, swap
        if (_heap[index].CompareTo(_heap[parentIndex]) < 0)
        {
            Swap(index, parentIndex);
            HeapifyUp(parentIndex);
        }
    }

    // Bubble element down to maintain min-heap property
    private void HeapifyDown(int index)
    {
        int leftChild = GetLeftChildIndex(index);
        int rightChild = GetRightChildIndex(index);
        int smallest = index;

        // Find the smallest among parent and children
        if (leftChild < _heap.Count && _heap[leftChild].CompareTo(_heap[smallest]) < 0)
            smallest = leftChild;

        if (rightChild < _heap.Count && _heap[rightChild].CompareTo(_heap[smallest]) < 0)
            smallest = rightChild;

        // If smallest is not the parent, swap and continue
        if (smallest != index)
        {
            Swap(index, smallest);
            HeapifyDown(smallest);
        }
    }

    // Helper methods for array-based heap navigation
    private static int GetParentIndex(int childIndex) => (childIndex - 1) / 2;
    private static int GetLeftChildIndex(int parentIndex) => 2 * parentIndex + 1;
    private static int GetRightChildIndex(int parentIndex) => 2 * parentIndex + 2;

    private void Swap(int i, int j)
    {
        (_heap[i], _heap[j]) = (_heap[j], _heap[i]);
    }
}