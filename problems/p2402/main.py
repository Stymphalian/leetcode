from heapq import heappush, heappop
from typing import *

class Solution:
    def editorial(self, n: int, meetings: List[List[int]]) -> int:
        meetings = sorted(meetings)
        used = []
        unused = list(range(n))
        meeting_count = [0] * n

        for (start, end) in meetings:
            while used and used[0][0] <= start:
                _, room = heappop(used)
                heappush(unused, room)

            if unused:
                room = heappop(unused)
                heappush(used, (end, room))
            else:
                next_time, room = heappop(used)
                heappush(used, (next_time + (end-start), room))
            
            meeting_count[room] += 1

        for i,v in enumerate(meeting_count):
            if v == max(meeting_count):
                return i
        return -1
    
    def mine(self, n: int, meetings: List[List[int]]) -> int:
        meetings = sorted(meetings)
        pq = []
        rooms = [(x,0) for x in range(n)]
        best_room = 0
        best_count = 0
        current_time = -1

        for (start, end) in meetings:
            duration = end - start

            while pq and pq[0][0] <= max(current_time, start):
                _, room, used = heappop(pq)
                heappush(rooms, (room, used + 1))

            if len(rooms) == 0:
                last_end, room, used = heappop(pq)
                current_time = last_end
                heappush(rooms, (room, used+1))

            room, used = heappop(rooms)
            current_time = max(current_time, start)

            # print(f"{current_time}, {room}, {used}, {duration}")
            if (used >= best_count):
                best_room = min(room, best_room) if (used == best_count) else room
                best_count = used
                
            heappush(pq, (current_time + duration, room, used + 1))


        while pq:
            _, room, used = heappop(pq)
            heappush(rooms, (room, used + 1))
        while rooms:
            room, used = heappop(rooms)
            if used >= best_count:
                best_room = min(room, best_room) if (used == best_count) else room
                best_count = used
        
        return best_room
    
    def mostBooked(self, n: int, meetings: List[List[int]]) -> int:
        return self.editorial(n, meetings)


s = Solution()
print(s.mostBooked(2, [[0,10],[1,5],[2,7],[3,4]]))           # 0
print(s.mostBooked(3, [[1,20],[2,10],[3,5],[4,9],[6,8]]))  # 1
print(s.mostBooked(4, [[0,2],[1,2],[2,4],[4,6]]))          # 0
print(s.mostBooked(2, [[4,11],[1,13],[8,15],[9,18],[0,17]])) # 1
print(s.mostBooked(3, [
    [0,10],
    [1,10],
    [2,10],
    [3,1000],
    [4,1000],
    [5,1000],
    [6,7],
    [1001,1005],
]))
