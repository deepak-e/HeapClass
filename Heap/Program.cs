//Impement Heap<T> tree using list<T>
//Heap rules: Node should be less than the children
//Method Add() -> SiftUp is required such that the node's value is less than children
//Method Remove() -> remove from the leaf.  -> SiftDown is required
//TEST: When removing the order will be in sorted order. Compare dotnet's List vs Heap implementation

#region ----- TESTING -----------
int Count = 0;
for (int ii = 0; ii < 5;  ii++) {
    Random random = new Random();
    Heap<int> heap = new();
    List<int> list = new List<int>();

    for (int i = 0; i < 1000; i++) {
        if (new Random().NextDouble() > (0.8 - ii * 0.1)) {
            var n = random.Next(1, 100 * (ii + 1));
            heap.Add(n);
            list.Add(n);
            list.Sort();
        } else {
            try {
                var Val = heap.Remove();
                var listVal = list[0];
                list.RemoveAt(0);
                Console.WriteLine($"{++Count:D4}. Heap {Val:D4} == List {listVal:D4}   --- Count={heap.Count():D3}");

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
            if (list[parent].CompareTo(list[index]) == 1) {
                (list[parent], list[index]) = (list[index], list[parent]); //swap
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
        int child = ((index + 1) * 2 - 1);

        if ((child < Ct) && list[index].CompareTo(list[child]) == 1) {   //compare with left child
            (list[index], list[child]) = (list[child], list[index]);
            SiftDown(child);  //travers left child after swap
        }

        if ((child + 1 < Ct) && list[index].CompareTo(list[child + 1]) == 1) {  //compare with right child
            (list[index], list[child + 1]) = (list[child + 1], list[index]);
            SiftDown(child + 1);  //traverse right child after swap
        }
    }
}
#endregion