function keys(t)
  local u = {}

  for k,_ in pairs(t) do
    table.insert(u, k)
  end

  return u
end

function intersect(a, b)
  local t = {}

  for _,v in pairs(a) do
    for _,w in pairs(b) do
      if v == w then
        t[v] = true
        break
      end
    end
  end

  return keys(t)
end

function priority(ch)
  if ch == ch:upper() then return string.byte(ch) - string.byte('A') + 27 end
  return string.byte(ch) - string.byte('a') + 1
end

function str2tbl(str)
  local t = {}
  str:gsub('.', function (x) table.insert(t, x) end)
  return t
end

local sacks = {}

for line in io.lines() do
  local mid = math.floor(line:len() / 2)
  local left, right = line:sub(1, mid), line:sub(mid + 1)

  table.insert(sacks, { full = str2tbl(line), left = str2tbl(left), right = str2tbl(right) })
end

local sum = 0

for _,s in pairs(sacks) do
  local inter = intersect(s.left, s.right)

  for _,ch in pairs(inter) do
    sum = sum + priority(ch)
  end
end

print(sum)

local grp = {}
sum = 0

for _,s in pairs(sacks) do
  table.insert(grp, s.full)

  if #grp == 3 then
    local inter = intersect(intersect(grp[1], grp[2]), grp[3])

    for _,ch in pairs(inter) do
      sum = sum + priority(ch)
    end

    grp = {}
  end
end

print(sum)
