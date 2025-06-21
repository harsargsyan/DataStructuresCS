namespace DataStructuresCS.BinaryTree;

public class AvlTree
{
    private class Node
    {
        public int Value;
        public Node? Left;
        public Node? Right;
        public int Height;

        public Node(int value)
        {
            Value = value;
            Height = 1;
        }
    }

    private Node? root;

    /// <summary>
    /// Searches for a value in the AVL Tree.
    /// Time Complexity: O(log n) where n is the number of nodes in the tree
    /// </summary>
    /// <param name="value">The value to search for</param>
    /// <returns>True if the value exists, false otherwise</returns>the
    public bool Search(int value) => SearchNode(root, value) != null;

    private static Node? SearchNode(Node? node, int value)
    {
        if (node == null || node.Value == value)
            return node;
        return value < node.Value ? SearchNode(node.Left, value) : SearchNode(node.Right, value);
    }

    /// <summary>
    /// Inserts a value into the AVL Tree.
    /// Time Complexity: O(log n) where n is the number of nodes in the tree
    /// </summary>
    /// <param name="value">The value to insert</param>
    public void Insert(int value)
    {
        root = InsertNode(root, value);
    }

    private static Node InsertNode(Node? node, int value)
    {
        // Standard BST insertion
        if (node == null)
            return new Node(value);

        if (value < node.Value)
            node.Left = InsertNode(node.Left, value);
        else if (value > node.Value)
            node.Right = InsertNode(node.Right, value);
        else
            return node; // Duplicate values not allowed

        // Update height of current node
        node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

        // Get balance factor
        int balance = GetBalance(node);

        // Left Case
        if (balance > 1 && value < node.Left!.Value)
            return RotateRight(node);

        // Right, Right Case
        if (balance < -1 && value > node.Right!.Value)
            return RotateLeft(node);

        // Left Right Case
        if (balance > 1 && value > node.Left!.Value)
        {
            node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }

        // Right Left Case
        if (balance < -1 && value < node.Right!.Value)
        {
            node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }

        return node;
    }

    /// <summary>
    /// Deletes a value from the AVL Tree.
    /// Time Complexity: O(log n) where n is the number of nodes in the tree
    /// </summary>
    /// <param name="value">The value to delete</param>
    public void Delete(int value)
    {
        root = DeleteNode(root, value);
    }

    private static Node? DeleteNode(Node? node, int value)
    {
        // Standard BST deletion
        if (node == null)
            return node;

        if (value < node.Value)
            node.Left = DeleteNode(node.Left, value);
        else if (value > node.Value)
            node.Right = DeleteNode(node.Right, value);
        else
        {
            // Node to be deleted found
            if (node.Left == null || node.Right == null)
            {
                Node? temp = node.Left ?? node.Right;
                if (temp == null)
                {
                    temp = node;
                    node = null;
                }
                else
                    node = temp;
            }
            else
            {
                // Node with two children: Get the inorder successor
                Node temp = FindMin(node.Right);
                node.Value = temp.Value;
                node.Right = DeleteNode(node.Right, temp.Value);
            }
        }

        if (node == null)
            return node;

        // Update height of current node
        node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

        // Get balance factor
        int balance = GetBalance(node);

        // Left Case
        if (balance > 1 && GetBalance(node.Left) >= 0)
            return RotateRight(node);

        // Left Right Case
        if (balance > 1 && GetBalance(node.Left) < 0)
        {
            if (node.Left != null) 
            {
                node.Left = RotateLeft(node.Left);
            }
            return RotateRight(node);
        }

        // Right, Right Case
        if (balance < -1 && GetBalance(node.Right) <= 0)
            return RotateLeft(node);

        // Right Left Case
        if (balance < -1 && GetBalance(node.Right) > 0)
        {
            if (node.Right != null)
            {
                node.Right = RotateRight(node.Right);
            }
            return RotateLeft(node);
        }

        return node;
    }

    /// <summary>
    /// Gets the height of the tree.
    /// Time Complexity: O(1)
    /// </summary>
    /// <returns>The height of the tree</returns>
    public int GetTreeHeight() => GetHeight(root);

    /// <summary>
    /// Checks if the tree is balanced (AVL property).
    /// Time Complexity: O(n) where n is the number of nodes in the tree
    /// </summary>
    /// <returns>True if the tree is balanced, false otherwise</returns>
    public bool IsBalanced() => IsBalanced(root);

    private static bool IsBalanced(Node? node)
    {
        if (node == null)
            return true;

        var balance = GetBalance(node);
        return Math.Abs(balance) <= 1 && IsBalanced(node.Left) && IsBalanced(node.Right);
    }

    /// <summary>
    /// Performs inorder traversal of the tree.
    /// Time Complexity: O(n) where n is the number of nodes in the tree
    /// </summary>
    /// <returns>List of values in inorder sequence</returns>
    public List<int> InorderTraversal()
    {
        var result = new List<int>();
        InorderTraversal(root, result);
        return result;
    }

    private static void InorderTraversal(Node? node, List<int> result)
    {
        if (node != null)
        {
            InorderTraversal(node.Left, result);
            result.Add(node.Value);
            InorderTraversal(node.Right, result);
        }
    }

    // Helper methods
    private static int GetHeight(Node? node) => node?.Height ?? 0;

    private static int GetBalance(Node? node)
    {
        if (node == null)
            return 0;
        return GetHeight(node.Left) - GetHeight(node.Right);
    }

    private static Node RotateRight(Node y)
    {
        Node x = y.Left!;
        Node? T2 = x.Right;

        // Perform rotation
        x.Right = y;
        y.Left = T2;

        // Update heights
        y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
        x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

        return x;
    }

    private static Node RotateLeft(Node x)
    {
        Node y = x.Right!;
        Node? t2 = y.Left;

        // Perform rotation
        y.Left = x;
        x.Right = t2;

        // Update heights
        x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
        y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

        return y;
    }

    private static Node FindMin(Node node)
    {
        while (node.Left != null)
            node = node.Left;
        return node;
    }
}