public class MyTree
{
    static Node root;

    public class Node 
    {
        public int data;
        public Node left, right;
    
        public Node(int item)
        {
            data = item;
            left = right = null;
        }
    }

    // Function to insert element in binary tree
    // Time complexity: O(n)
    static void Insert(Node temp, int key)
    {
        if (temp == null)
        {
            root = new Node(key);
            return;
        }
        Queue<Node> q = new Queue<Node>();
        q.Enqueue(temp); 
        // Do level order traversal until we find
        // an empty place.
        while (q.Count != 0)
        {
            temp = q.Peek();
            q.Dequeue(); 
            if (temp.left == null)
            {
                temp.left = new Node(key);
                break;
            }
            else
            {
                q.Enqueue(temp.left);
            }
            if (temp.right == null)
            {
                temp.right = new Node(key);
                break;
            }
            else
            {
                q.Enqueue(temp.right);
            }                
        }
    }

    // Inorder traversal of a binary tree 
    static void Inorder(Node temp) 
    { 
        if (temp == null)
        {
            return;
        }  
        Inorder(temp.left); 
        Console.Write(temp.data + " "); 
        Inorder(temp.right); 
    }

    // Function to delete deepest element in binary tree
    // Time complexity: O(n)
    static void DeleteDeepest(Node root, Node delNode) 
    { 
        Queue<Node> q = new Queue<Node>(); 
        q.Enqueue(root);  
        Node temp = null;          
        // Do level order traversal until last node 
        while (q.Count != 0)
        {
            temp = q.Peek(); 
            q.Dequeue(); 
  
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
                else
                {
                    q.Enqueue(temp.right); 
                }
            }  
            if (temp.left != null)
            { 
                if (temp.left == delNode)
                { 
                    temp.left = null; 
                    return; 
                } 
                else
                {
                    q.Enqueue(temp.left);
                }
            } 
        } 
    }

    // Function to delete given element in binary tree 
    static void Delete(Node root, int key)
    {
        if (root == null)
        {
            return;
        }  
        if (root.left == null && root.right == null)
        { 
            if (root.data == key)
            { 
                root = null; 
                return; 
            } 
            else
            {
                return;
            }
        } 
  
        Queue<Node> q = new Queue<Node>(); 
        q.Enqueue(root); 
        Node temp = null, keyNode = null; 
  
        // Do level order traversal until 
        // we find key and last node. 
        while (q.Count != 0)
        { 
            temp = q.Peek(); 
            q.Dequeue(); 
  
            if (temp.data == key) 
                keyNode = temp; 
  
            if (temp.left != null) 
                q.Enqueue(temp.left); 
  
            if (temp.right != null) 
                q.Enqueue(temp.right); 
        } 
  
        if (keyNode != null) 
        { 
            int x = temp.data; 
            DeleteDeepest(root, temp); 
            keyNode.data = x; 
        } 
    }

    /* Compute the "maxDepth" of a tree -- the number of
    nodes along the longest path from the root node
    down to the farthest leaf node.*/
    int MaxDepth(Node node)
    {
        if (node == null)
            return 0;
        else {
            /* compute the depth of each subtree */
            int lDepth = MaxDepth(node.left);
            int rDepth = MaxDepth(node.right);
 
            /* use the larger one */
            if (lDepth > rDepth)
                return (lDepth + 1);
            else
                return (rDepth + 1);
        }
    }
}