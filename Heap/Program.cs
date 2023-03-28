//Impement Heap<T> tree using list<T>
//Heap rules: Node should be less than the children
//Method Add() -> SiftUp is required such that the node's value is less than children
//Method Remove() -> remove from the leaf.  -> SiftDown is required
//TEST: When removing the order will be in sorted order. Compare dotnet's List vs Heap implementation

#region ----- TESTING -----------
int Count = 0;
for (int ii = 0; ii < 10;  ii++) {
    Random random = new Random();
    Heap<int> heap = new();
    List<int> list = new List<int>();

    for (int i = 0; i < 500 ; i++) {
        if (new Random().NextDouble() > (0.8 - ii * 0.05)) {
            var n = random.Next(1, 10 * (ii + 10));
            heap.Add(n);
            list.Add(n);
        } else {
            try {
                var Val = heap.Remove();
                list.Sort();
                var listVal = list[0];
                list.RemoveAt(0);
                Console.WriteLine($"{++Count:D4}. Heap {Val:D4} == List {listVal:D4}   --- H.Count={heap.Count():D3}");

                if (Val != listVal) { Console.WriteLine("Error!!!"); Console.ReadLine(); }
            } catch (Exception ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }
    }

    Console.WriteLine($"Heap size {heap.Count()}");
}
#endregion


#region Heap Class-----------
public class Heap<T> where T: IComparable<T> {

    List<T> list;
    int Ct;
    public Heap() => list = new();
    public int Count() => list.Count;
    public void Add(T item) {
        if (list.Count == Ct)
            list.Add(item);
        else list[Ct] = item;
        Ct++;
        SiftUp(Ct - 1);
    }
    void SiftUp(int index) {
        int parent = ((index + 1) / 2 - 1);

        if (parent >= 0) {
            if (list[parent].CompareTo(list[index]) == 1) { //swap if parent > child
                (list[parent], list[index]) = (list[index], list[parent]);
                SiftUp(parent);
            }
        }
    }
    public T Remove() {
        if (Ct == 0) throw new Exception("Heap empty");
        T item = list[0]; //root
        (list[0], list[Ct - 1]) = (list[Ct - 1], list[0]);
        Ct--;
        SiftDown(0);
        return item;
    }
    void SiftDown(int index) {
        int leftChild = ((index + 1) * 2 - 1);
        int rightChild = leftChild + 1;
        int miniChild = index;

        if (rightChild < Ct ) //compare left and right child and determine the min child
            miniChild = (list[leftChild].CompareTo(list[rightChild]) == 1)? rightChild : leftChild;
        else if (leftChild < Ct) //if only left exists
            miniChild = leftChild;

        if (miniChild != index && list[index].CompareTo(list[miniChild]) == 1) {
            (list[index], list[miniChild]) = (list[miniChild], list[index]);
            SiftDown(miniChild);
        }
    }
}
#endregion