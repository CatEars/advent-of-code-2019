import turtle

with open('in.real', 'r') as f:
    lines = f.readlines()

first = [(x[0], int(x[1:])) for x in lines[0].split(',')]
second = [(x[0], int(x[1:])) for x in lines[1].split(',')]
decision_matrix = {
    'U': (0, 1),
    'D': (0, -1),
    'R': (1, 0),
    'L': (-1, 0)
}

steps = {}
current_steps = 0
x, y = 0, 0
for (d, length) in first:
    dx, dy = decision_matrix[d]
    for _ in range(length):
        current_steps += 1
        x, y = x + dx, y + dy
        steps[(x, y)] = steps.get((x, y), current_steps)

steps2 = {}
crossings = []
current_steps = 0
x, y = 0, 0
for (d, length) in second:
    dx, dy = decision_matrix[d]
    for _ in range(length):
        current_steps += 1
        x, y = x + dx, y + dy
        steps2[(x, y)] = steps2.get((x, y), current_steps)
        if (x, y) in steps:
            crossings.append((steps[(x, y)] + steps2[(x, y)], (x, y)))
    
print(min(crossings))
print(crossings)


