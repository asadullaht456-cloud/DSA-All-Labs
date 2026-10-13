namespace DSALabsApi.Services
{
    public class SinglyLinkedList
    {
        private class Node
        {
            public int Data;
            public Node? Next;
            public Node(int data) { Data = data; Next = null; }
        }

        private Node? _head;
        private int _size;

        public int Size => _size;

        public int[] Traverse()
        {
            var list = new System.Collections.Generic.List<int>();
            var current = _head;
            while (current != null)
            {
                list.Add(current.Data);
                current = current.Next;
            }
            return list.ToArray();
        }

        public void InsertAtBeginning(int value)
        {
            var node = new Node(value);
            node.Next = _head;
            _head = node;
            _size++;
        }

        public void InsertAtEnd(int value)
        {
            var node = new Node(value);
            if (_head == null) { _head = node; _size++; return; }

            var current = _head;
            while (current.Next != null) current = current.Next;
            current.Next = node;
            _size++;
        }

        public (bool ok, string msg) InsertAtPosition(int pos, int value)
        {
            if (pos < 0 || pos > _size)
                return (false, $"Invalid position. Valid range: 0 to {_size}.");

            if (pos == 0) { InsertAtBeginning(value); return (true, "Inserted at position 0."); }

            var current = _head;
            for (int i = 0; i < pos - 1 && current != null; i++)
                current = current.Next;

            if (current == null) return (false, "Position out of range.");

            var node = new Node(value);
            node.Next = current.Next;
            current.Next = node;
            _size++;
            return (true, $"Inserted {value} at position {pos}.");
        }

        public (bool ok, string msg) DeleteFromBeginning()
        {
            if (_head == null) return (false, "Underflow: list is empty.");
            _head = _head.Next;
            _size--;
            return (true, "Removed first node.");
        }

        public (bool ok, string msg) DeleteFromEnd()
        {
            if (_head == null) return (false, "Underflow: list is empty.");
            if (_head.Next == null) { _head = null; _size--; return (true, "Removed last node."); }

            var current = _head;
            while (current.Next!.Next != null) current = current.Next;
            current.Next = null;
            _size--;
            return (true, "Removed last node.");
        }

        public (bool ok, string msg) DeleteAtPosition(int pos)
        {
            if (_head == null) return (false, "Underflow: list is empty.");
            if (pos < 0 || pos >= _size)
                return (false, $"Invalid position. Valid range: 0 to {_size - 1}.");

            if (pos == 0) return DeleteFromBeginning();

            var current = _head;
            for (int i = 0; i < pos - 1; i++) current = current.Next!;
            current.Next = current.Next!.Next;
            _size--;
            return (true, $"Removed node at position {pos}.");
        }

        public int Search(int key)
        {
            var current = _head;
            int idx = 0;
            while (current != null)
            {
                if (current.Data == key) return idx;
                current = current.Next;
                idx++;
            }
            return -1;
        }

        public (bool ok, string msg) UpdateAtPosition(int pos, int newValue)
        {
            if (pos < 0 || pos >= _size)
                return (false, $"Invalid position. Valid range: 0 to {_size - 1}.");

            var current = _head;
            for (int i = 0; i < pos; i++) current = current!.Next;
            current!.Data = newValue;
            return (true, $"Updated position {pos} to {newValue}.");
        }

        public void Clear() { _head = null; _size = 0; }
    }
}
