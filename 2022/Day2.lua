function getResult(a, b)
  local diff = b - a

  if diff == 0 then return 3
  elseif diff == 1 then return 6
  elseif diff == -2 then return 6
  else return 0 end
end

local matches = {}

for line in io.lines() do
  table.insert(matches,
    { string.byte(line:sub(1, 2)) - string.byte('A'), string.byte(line:sub(3)) - string.byte('X') })
end

local sum = 0
for _,v in pairs(matches) do
  sum = sum + getResult(v[1], v[2]) + v[2] + 1
end

print(sum)

sum = 0
for _,v in pairs(matches) do
  local result = 0

  if v[2] == 1 then result = 3
  elseif v[2] == 2 then result = 6 end

  local shape = 0

  if result == 3 then shape = v[1]
  elseif result == 6 then shape = (v[1] + 1) % 3
  else shape = (v[1] - 1) % 3 end

  sum = sum + result + shape + 1
end

print(sum)
