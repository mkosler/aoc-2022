function getSize(file)
  if file.t == "file" then return file.size end

  local size = 0

  for _,v in pairs(file.children) do
    size = size + getSize(v)
  end

  return size
end

function getAllSizes(current, sizes)
  sizes = sizes or {}

  if current.t == "dir" then
    table.insert(sizes, getSize(current))
  end

  for _,v in pairs(current.children) do
    getAllSizes(v, sizes)
  end
end

local SMALLER_THAN = 100000
local TOTAL_FILESYSTEM_SIZE = 70000000
local TOTAL_UPDATE_SIZE = 30000000

local root = {
  name = "/",
  children = {},
  parent = nil,
  t = "dir",
  size = 0,
}

local current = nil

for line in io.lines() do
  if line:find("$ cd") then
    local name = line:sub(6)

    if name == ".." then
      current = current.parent
    elseif name == "/" then
      current = root
    else
      current = current.children[name]
    end
  elseif not line:find("%$") then
    local first, second = line:match("(.+) (.+)")

    if first:find("dir") then
      if not current.children[second] then
        current.children[second] = {
          name = second,
          children = {},
          parent = current,
          t = "dir",
          size = 0
        }
      end
    else
      current.children[second] = {
        name = second,
        children = {},
        parent = current,
        t = "file",
        size = tonumber(first)
      }
    end
  end
end

local sizes = {}
getAllSizes(root, sizes)

local total = 0

for _,v in pairs(sizes) do
  if v <= SMALLER_THAN then
    total = total + v
  end
end

print(total)

local remainingToUpdate = TOTAL_UPDATE_SIZE - (TOTAL_FILESYSTEM_SIZE - getSize(root))

local min = math.huge

for _,v in pairs(sizes) do
  if v >= remainingToUpdate and v < min then
    min = v
  end
end

print(min)
