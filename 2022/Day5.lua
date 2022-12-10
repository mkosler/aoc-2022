local initial = {}
local moves = {}
local stacks = {}
local current = initial

for line in io.lines() do
  if line == "" then
    current = moves
  else
    table.insert(current, line)
  end
end

for i = #initial, 1, -1 do
  local l = initial[i]

  if l:find("%[") then
    for i = 1, l:len(), 4 do
      local maybe = l:sub(i, i + 3)

      if maybe:find("%S") then
        local j = math.floor(i / 4) + 1

        local m = maybe:match("%[(.)%]")
        table.insert(stacks[j], m)
      end
    end
  else
    local count = tonumber(l:sub(-2))

    for i = 1, count do
      table.insert(stacks, {})
    end
  end
end

for _,m in ipairs(moves) do
  local count, from, to = m:match("move (%d+) from (%d+) to (%d+)")
  count, from, to = tonumber(count), tonumber(from), tonumber(to)

  for i = 1, count do
    table.insert(stacks[to], table.remove(stacks[from]))
  end
end

local result = ""

for _,s in ipairs(stacks) do
  result = result .. s[#s]
end

print(result)

stacks = {}

for i = #initial, 1, -1 do
  local l = initial[i]

  if l:find("%[") then
    for i = 1, l:len(), 4 do
      local maybe = l:sub(i, i + 3)

      if maybe:find("%S") then
        local j = math.floor(i / 4) + 1

        local m = maybe:match("%[(.)%]")
        table.insert(stacks[j], m)
      end
    end
  else
    local count = tonumber(l:sub(-2))

    for i = 1, count do
      table.insert(stacks, {})
    end
  end
end

for _,m in ipairs(moves) do
  local count, from, to = m:match("move (%d+) from (%d+) to (%d+)")
  count, from, to = tonumber(count), tonumber(from), tonumber(to)

  local t = {}
  for i = 1, count do
    table.insert(t, table.remove(stacks[from]))
  end

  for i = 1, count do
    table.insert(stacks[to], table.remove(t))
  end
end

local result = ""

for _,s in ipairs(stacks) do
  result = result .. s[#s]
end

print(result)
