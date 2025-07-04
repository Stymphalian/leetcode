from typing import *

class Solution:
    def solve(self, k: int, operations: List[int]) -> str:
        if len(operations) == 0:
            return 'a'
        
        current_len = (1 << len(operations))
        prev_len = current_len // 2

        # Recursively find the value of k until we reach 1.
        k_prime = k if k < prev_len else k - prev_len
        c = self.solve(k_prime, operations[:-1])

        if operations[-1] == 0:
            return c
        else:
            if k < prev_len:
                return c
            else:
                return chr(((ord(c) - ord('a')) + 1)%26 + ord('a'))

    def kthCharacter(self, k: int, operations: List[int]) -> str:
        return self.solve(k-1, operations)

s = Solution()
cases = [
    ([0,0,0], 5),    # a
    ([0,1,0,1], 10), # b
    ([0,0,1], 4),    # a
]
for a, k in cases:
    print(s.kthCharacter(k, a))