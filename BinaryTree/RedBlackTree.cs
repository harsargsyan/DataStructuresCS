namespace DataStructuresCS.BinaryTree;

public class RedBlackTree
{
    private enum Color { Red, Black }

    private class Node
    {
        public readonly int Value;
        public Color Color;
        public Node? Left;
        public Node? Right;
        public Node? Parent;

        public Node(int value, Color color = Color.Red)
        {
            Value = value;
            Color = color;
        }
    }

    private Node? root;

    /// <summary>
    /// Searches for a value in the Red-Black Tree.
    /// Time Complexity: O(log n) where n is the number of nodes in the tree
    /// </summary>
    /// <param name="value">The value to search for</param>
    /// <returns>True if the value exists, false otherwise</returns>
    public bool Search(int value) => SearchNode(root, value) != null;

    private static Node? SearchNode(Node? node, int value)
    {
        if (node == null || node.Value == value)
            return node;
        return value < node.Value ? SearchNode(node.Left, value) : SearchNode(node.Right, value);
    }

    /// <summary>
    /// Inserts a value into the Red-Black Tree.
    /// Time Complexity: O(log n) where n is the number of nodes in the tree
    /// </summary>
    /// <param name="value">The value to insert</param>
    public void Insert(int value)
    {
        var newNode = new Node(value);

        if (root == null)
        {
            root = newNode;
            root.Color = Color.Black;
            return;
        }

        Node? parent = null;
        var current = root;
        while (current != null)
        {
            parent = current;
            if (value < current.Value)
                current = current.Left;
            else if (value > current.Value)
                current = current.Right;
            else
                return;
        }

        newNode.Parent = parent;
        if (value < parent!.Value)
            parent.Left = newNode;
        else
            parent.Right = newNode;


        FixInsert(newNode);
    }

    private void FixInsert(Node node)
    {
        while (node != root && node.Parent != null && node.Parent.Color == Color.Red)
        {
            if (node.Parent.Parent == null)
                break;

            if (node.Parent == node.Parent.Parent.Left)
            {
                Node? uncle = node.Parent.Parent.Right;

                if (uncle != null && uncle.Color == Color.Red)
                {
                    node.Parent.Color = Color.Black;
                    uncle.Color = Color.Black;
                    node.Parent.Parent.Color = Color.Red;
                    node = node.Parent.Parent;
                }
                else
                {
                    if (node == node.Parent.Right)
                    {
                        node = node.Parent;
                        RotateLeft(node);
                    }

                    node.Parent!.Color = Color.Black;
                    node.Parent!.Parent!.Color = Color.Red;
                    RotateRight(node.Parent.Parent);
                }
            }
            else
            {
                Node? uncle = node.Parent.Parent.Left;

                if (uncle != null && uncle.Color == Color.Red)
                {
                    node.Parent.Color = Color.Black;
                    uncle.Color = Color.Black;
                    node.Parent.Parent.Color = Color.Red;
                    node = node.Parent.Parent;
                }
                else
                {
                    if (node == node.Parent.Left)
                    {
                        node = node.Parent;
                        RotateRight(node);
                    }

                    node.Parent!.Color = Color.Black;
                    node.Parent!.Parent!.Color = Color.Red;
                    RotateLeft(node.Parent.Parent);
                }
            }
        }

        root!.Color = Color.Black;
    }


    /// <summary>
    /// Deletes a value from the Red-Black Tree.
    /// Time Complexity: O(log n) where n is the number of nodes in the tree
    /// </summary>
    /// <param name="value">The value to delete</param>
    public void Delete(int value)
    {
        var node = SearchNode(root, value);
        if (node != null)
            DeleteNode(node);
    }

    private void DeleteNode(Node node)
    {
        Node? replacement;
        var originalColor = node.Color;

        if (node.Left == null)
        {
            replacement = node.Right;
            Transplant(node, node.Right);
        }
        else if (node.Right == null)
        {
            replacement = node.Left;
            Transplant(node, node.Left);
        }
        else
        {
            var successor = FindMin(node.Right);
            originalColor = successor.Color;
            replacement = successor.Right;

            if (successor.Parent == node)
            {
                if (replacement != null)
                    replacement.Parent = successor;
            }
            else
            {
                Transplant(successor, successor.Right);
                successor.Right = node.Right;
                if (successor.Right != null)
                    successor.Right.Parent = successor;
            }

            Transplant(node, successor);
            successor.Left = node.Left;
            if (successor.Left != null)
                successor.Left.Parent = successor;
            successor.Color = node.Color;
        }

        if (originalColor == Color.Black && replacement != null)
            FixDelete(replacement);
    }

    private void FixDelete(Node? node)
{
    while (node != null && node != root && GetColor(node) == Color.Black)
    {
        if (node.Parent == null)
            break;
            
        if (node == node.Parent.Left)
        {
            Node? sibling = node.Parent.Right;
            if (sibling == null)
                break;
                
            if (GetColor(sibling) == Color.Red)
            {
                sibling.Color = Color.Black;
                node.Parent.Color = Color.Red;
                RotateLeft(node.Parent);
                sibling = node.Parent.Right;
                if (sibling == null)
                    break;
            }

            bool siblingLeftBlack = GetColor(sibling.Left) == Color.Black;
            bool siblingRightBlack = GetColor(sibling.Right) == Color.Black;
            
            if (siblingLeftBlack && siblingRightBlack)
            {
                sibling.Color = Color.Red;
                node = node.Parent;
            }
            else
            {
                if (siblingRightBlack)
                {
                    if (sibling.Left != null)
                        sibling.Left.Color = Color.Black;
                    sibling.Color = Color.Red;
                    RotateRight(sibling);
                    sibling = node.Parent.Right;
                    if (sibling == null)
                        break;
                }

                sibling.Color = node.Parent.Color;
                node.Parent.Color = Color.Black;
                if (sibling.Right != null)
                    sibling.Right.Color = Color.Black;
                RotateLeft(node.Parent);
                node = root;
            }
        }
        else // node is right child
        {
            Node? sibling = node.Parent.Left;
            if (sibling == null)
                break;
                
            if (GetColor(sibling) == Color.Red)
            {
                sibling.Color = Color.Black;
                node.Parent.Color = Color.Red;
                RotateRight(node.Parent);
                sibling = node.Parent.Left;
                if (sibling == null)
                    break;
            }

            var siblingRightBlack = GetColor(sibling.Right) == Color.Black;
            var siblingLeftBlack = GetColor(sibling.Left) == Color.Black;
            
            if (siblingRightBlack && siblingLeftBlack)
            {
                sibling.Color = Color.Red;
                node = node.Parent;
            }
            else
            {
                if (siblingLeftBlack)
                {
                    if (sibling.Right != null)
                        sibling.Right.Color = Color.Black;
                    sibling.Color = Color.Red;
                    RotateLeft(sibling);
                    sibling = node.Parent.Left;
                    if (sibling == null)
                        break;
                }

                sibling.Color = node.Parent.Color;
                node.Parent.Color = Color.Black;
                if (sibling.Left != null)
                    sibling.Left.Color = Color.Black;
                RotateRight(node.Parent);
                node = root;
            }
        }
    }

    if (node != null)
        node.Color = Color.Black;
}
    
    // Rotations
    private void RotateLeft(Node? node)
    {
        if (node == null || node.Right == null)
            return;

        Node right = node.Right;
        node.Right = right.Left;
        if (right.Left != null)
            right.Left.Parent = node;

        right.Parent = node.Parent;

        if (node.Parent == null)
            root = right;
        else if (node == node.Parent.Left)
            node.Parent.Left = right;
        else
            node.Parent.Right = right;

        right.Left = node;
        node.Parent = right;
    }
    
    private void RotateRight(Node? node)
    {
        if (node == null || node.Left == null)
            return;
        
        var left = node.Left;
        node.Left = left.Right;
        if (left.Right != null)
            left.Right.Parent = node;

        left.Parent = node.Parent;

        if (node.Parent == null)
            root = left;
        else if (node == node.Parent.Right)
            node.Parent.Right = left;
        else
            node.Parent.Left = left;

        left.Right = node;
        node.Parent = left;
    }
    
    private void Transplant(Node u, Node? v)
    {
        if (u.Parent == null)
            root = v;
        else if (u == u.Parent.Left)
            u.Parent.Left = v;
        else
            u.Parent.Right = v;

        if (v != null)
            v.Parent = u.Parent;
    }

    private static Node FindMin(Node node)
    {
        while (node.Left != null)
            node = node.Left;
        return node;
    }

    private static Color GetColor(Node? node) => node?.Color ?? Color.Black;
}
