function distinct(l)
  local t = {}

  for i = 1, l:len() do
    t[l:sub(i, i)] = true
  end

  local u = {}

  for k,_ in pairs(t) do
    table.insert(u, k)
  end

  return u
end

function startOfPacket(l, length)
  for i = 1, l:len() - length - 1 do
    local maybe = l:sub(i, i + length - 1)
    local d = distinct(maybe)

    if maybe:len() == #d then
      return i + length - 1
    end
  end

  return -1
end

local line = io.read('l')

print(startOfPacket(line, 4))
print(startOfPacket(line, 14))
