public class MyTree
{
    private Node? root;

    private class Node
    {
        public int data;
        public Node? left, right;

        public Node(int item)
        {
            data = item;
            left = right = null;
        }
    }

    /// <summary>
    /// Inserts a node in level order.
    /// Time complexity: O(n), where n is the number of nodes in the tree.
    /// </summary>
    public void Insert(int key)
    {
        if (root == null)
        {
            root = new Node(key);
            return;
        }

        var q = new Queue<Node>();
        q.Enqueue(root);

        while (q.Count > 0)
        {
            Node temp = q.Dequeue();

            if (temp.left == null)
            {
                temp.left = new Node(key);
                return;
            }
            q.Enqueue(temp.left);

            if (temp.right == null)
            {
                temp.right = new Node(key);
                return;
            }
            q.Enqueue(temp.right);
        }
    }

    /// <summary>
    /// Deletes a node by replacing it with the deepest node.
    /// Time complexity: O(n), where n is the number of nodes in the tree.
    /// </summary>
    public void Delete(int key)
    {
        if (root == null) return;

        if (root.left == null && root.right == null)
        {
            if (root.data == key) root = null;
            return;
        }

        Queue<Node> q = new Queue<Node>();
        q.Enqueue(root);

        Node? keyNode = null;
        Node? temp = null;

        while (q.Count > 0)
        {
            temp = q.Dequeue();

            if (temp.data == key)
                keyNode = temp;

            if (temp.left != null) q.Enqueue(temp.left);
            if (temp.right != null) q.Enqueue(temp.right);
        }

        if (keyNode == null || temp == null) return;
        int x = temp.data;
        DeleteDeepest(root, temp);
        keyNode.data = x;
    }

    /// <summary>
    /// Deletes the deepest node in the tree.
    /// Time complexity: O(n), where n is the number of nodes in the tree.
    /// </summary>
    private void DeleteDeepest(Node root, Node delNode)
    {
        var q = new Queue<Node>();
        q.Enqueue(root);

        while (q.Count > 0)
        {
            Node temp = q.Dequeue();

            if (temp == delNode)
            {
                temp = null;
                return;
            }

            if (temp.right != null)
            {
                if (temp.right == delNode)
                {
                    temp.right = null;
                    return;
                }

                q.Enqueue(temp.right);
            }

            if (temp.left != null)
            {
                if (temp.left == delNode)
                {
                    temp.left = null;
                    return;
                }

                q.Enqueue(temp.left);
            }
        }
    }

    /// <summary>
    /// Computes the maximum depth of the tree.
    /// Time complexity: O(n), where n is the number of nodes in the tree.
    /// </summary>
    public int MaxDepth()
    {
        return MaxDepth(root);
    }

    private int MaxDepth(Node? node)
    {
        if (node == null) return 0;
        return Math.Max(MaxDepth(node.left), MaxDepth(node.right)) + 1;
    }

    // ======== DFS Traversals (Recursive) ========

    /// <summary>
    /// In-order traversal (Left, Root, Right).
    /// Time complexity: O(n), where n is the number of nodes in the tree.
    /// </summary>
    public void InOrderTraversal()
    {
        InOrder(root);
        Console.WriteLine();
    }

    private void InOrder(Node? node)
    {
        if (node == null) return;
        InOrder(node.left);
        Console.Write(node.data + " ");
        InOrder(node.right);
    }

    /// <summary>
    /// Pre-order traversal (Root, Left, Right).
    /// Time complexity: O(n), where n is the number of nodes in the tree.
    /// </summary>
    public void PreOrderTraversal()
    {
        PreOrder(root);
        Console.WriteLine();
    }

    private void PreOrder(Node? node)
    {
        if (node == null) return;
        Console.Write(node.data + " ");
        PreOrder(node.left);
        PreOrder(node.right);
    }

    /// <summary>
    /// Post-order traversal (Left, Right, Root).
    /// Time complexity: O(n), where n is the number of nodes in the tree.
    /// </summary>
    public void PostOrderTraversal()
    {
        PostOrder(root);
        Console.WriteLine();
    }

    private void PostOrder(Node? node)
    {
        if (node == null) return;
        PostOrder(node.left);
        PostOrder(node.right);
        Console.Write(node.data + " ");
    }

    // ======== DFS (Iterative Pre-order) ========

    /// <summary>
    /// Iterative pre-order traversal using a stack.
    /// Time complexity: O(n), where n is the number of nodes in the tree.
    /// </summary>
    public void IterativeDFS()
    {
        if (root == null) return;

        Stack<Node> stack = new Stack<Node>();
        stack.Push(root);

        while (stack.Count > 0)
        {
            Node current = stack.Pop();
            Console.Write(current.data + " ");

            if (current.right != null) stack.Push(current.right);
            if (current.left != null) stack.Push(current.left);
        }
        Console.WriteLine();
    }

    // ======== BFS Traversal ========

    /// <summary>
    /// Level-order traversal (Breadth-First Search).
    /// Time complexity: O(n), where n is the number of nodes in the tree.
    /// </summary>
    public void LevelOrderTraversal()
    {
        if (root == null) return;

        var q = new Queue<Node>();
        q.Enqueue(root);

        while (q.Count > 0)
        {
            Node temp = q.Dequeue();
            Console.Write(temp.data + " ");

            if (temp.left != null) q.Enqueue(temp.left);
            if (temp.right != null) q.Enqueue(temp.right);
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Level-order traversal with level tracking.
    /// Time complexity: O(n), where n is the number of nodes in the tree.
    /// </summary>
    public void LevelOrderWithLevels()
    {
        if (root == null) return;

        Queue<Node> q = new Queue<Node>();
        q.Enqueue(root);

        while (q.Count > 0)
        {
            int levelSize = q.Count;
            for (int i = 0; i < levelSize; i++)
            {
                Node node = q.Dequeue();
                Console.Write(node.data + " ");

                if (node.left != null) q.Enqueue(node.left);
                if (node.right != null) q.Enqueue(node.right);
            }
            Console.WriteLine();
        }
    }
}
