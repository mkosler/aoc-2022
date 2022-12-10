local calcs = {}
local current = 0

for line in io.lines() do
  if line ~= "" then
    current = current + tonumber(line)
  else
    table.insert(calcs, current)
    current = 0
  end
end

table.sort(calcs)
print(calcs[#calcs])
print(calcs[#calcs] + calcs[#calcs - 1] + calcs[#calcs - 2])
