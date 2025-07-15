public class Solution {
    public int GetDecimalValue(ListNode head) {
        uint number = 0;
        ListNode current = head;
        while(current != null) {
            number = (number << 1);
            number |= (uint) current.val;
            current = current.next;
        }
        return (int) number;
    }
}