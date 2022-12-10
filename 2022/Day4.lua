function isSubset(x1, x2, y1, y2)
  return x1 <= y1 and y2 <= x2
end

function overlaps(x1, x2, y1, y2)
  return x2 >= y1 and y2 >= x1
end

local count = 0
local oCount = 0

for line in io.lines() do
  local x1, x2, y1, y2 = line:match("(%d+)%-(%d+),(%d+)%-(%d+)")
  x1, x2, y1, y2 = tonumber(x1), tonumber(x2), tonumber(y1), tonumber(y2)

  if isSubset(x1, x2, y1, y2) or isSubset(y1, y2, x1, x2) then
    count = count + 1
  end

  if overlaps(x1, x2, y1, y2) then
    oCount = oCount + 1
  end
end

print(count)
print(oCount)
