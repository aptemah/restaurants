function ClassName(x) {
    return x ? (x.constructor ? x.constructor.toString().substring('function '.length, x.constructor.toString().indexOf('(')).trim() : undefined) : undefined;
}

function XFunc(func) {
    if (typeof func == 'object') {
        var funcBody = 'return true';
        for (var v in func) {
            if (typeof(func[v]) == 'string')
                funcBody += ' && x.' + v.replace(/\./g, '().') + '()=="' + String(func[v]).replace(/"/g, '\"') + '"';
            else
            	funcBody += ' && x.' + v.replace(/\./g, '().') + '()==' + String(func[v]);
        }
    	return Function('x', funcBody + ';');
    } else if (typeof func == 'string')
        return Function('x', 'y', 'z', 't', 'return ' + func + ';');
    else
        return func;
}

Array.prototype.ForEach = function (f) {
    f = XFunc(f);
    for (var i = 0; i < this.length; i++) {
        var ret = f.call(this[i], this[i], i);
        if (ret !== undefined)
            return ret;
    }
}

Array.prototype.Add = function (item) {
    this.push(item);
    return this;
}

Array.prototype.AddOnce = function (item) {
    if (!this.Contains(item)) {
        this.Add(item);
    }
    return this;
}

Array.prototype.Aggregate = function (seed, accumulator) { // accumulator is func(total, next, index). Example of using (facterial function): Aggregate(1, 'x * y')
    accumulator = XFunc(accumulator);
    var total = seed;
    this.ForEach(function (x, i) { total = accumulator(total, x, i); });
    return total;
}

Array.prototype.All = function (f) {
    f = XFunc(f);
    return !this.ForEach(function (x, i) { if (!f.call(x, x, i)) return true; });
}

Array.prototype.Any = function (f) {
    if (f) {
        f = XFunc(f);
        return Boolean(this.ForEach(function (x, i) { if (f.call(x, x, i)) return true; }));
    } else
        return Boolean(this.Count());
}

Array.prototype.Empty = function (f) {
    return !this.Any(f);
}

Array.prototype.Average = function (f) {
    if (f) return this.Where(f).Average();
    return this.Count() ? this.Sum() / this.Count() : undefined;
}

Array.prototype.CloneWithoutItems = function () {
    var result = [];
    for (var i in this)
        if (!/^\d+$/.test(i))
            result[i] = this[i];
    return result;
}

Array.prototype.Clone = function () {
    return this.Range();
}

Array.prototype.Concat = function (a) {
    var result = this.Clone();
    result.push.apply(result, a);
    for (var i in a)
        if (!/^\d+$/.test(i))
            result[i] = a[i];
    return result;
}

Array.prototype.Contains = function (x) {
    return Boolean(this.ForEach(function(a) { if (a === x) return true; }));
}

Array.prototype.Count = function (f) {
    return f ? this.Where(f).Count() : this.length;
}

Array.prototype.Cycle = function (shift) {
    shift %= this.Count();
    return this.Skip(-shift).Concat(this.Take(-shift));
}

Array.prototype.Distinct = function () {
    result = this.CloneWithoutItems();
    this.ForEach(function (x) { if (!result.Contains(x)) result.push(x); });
    return result;
}

Array.prototype.ElementAt = function (i) {
    return i >= 0 && i < this.Count() ? this[i] : undefined;
}

Array.prototype.Exclude = function (f) {
	var result = this.CloneWithoutItems();
	f = XFunc(f);
	this.ForEach(function (x, i) { if (!f.call(x, x, i)) result.push(x); });
	return result;
}

Array.prototype.Except = function (a) {
    if (ClassName(a) != 'Array')
        a = [a];
    return this.Where(function (x) { return !a.Contains(x) });
}

Array.prototype.IndexOf = function(x) {
    return this.FirstIndex(function(a) { return a === x; });
}

Array.prototype.First = function (f) {
    if (f) {
        f = XFunc(f);
        return this.ForEach(function (x, i) { if (f.call(x, x, i)) return x; });
    } else
        return this.ElementAt(0);
}

Array.prototype.FirstIndex = function (f) {
	if (f) {
		f = XFunc(f);
		return this.ForEach(function (x, i) { if (f.call(x, x, i)) return i; });
	} else
		if (this.Count())
      		return 0;
}

Array.prototype.LastIndex = function (f) {
    if (f) {
        f = XFunc(f);
        return this.Reverse().ForEach(function (x, i) { if (f.call(x, x, i)) return i; });
    } else
        if (this.Count())
            return 0;
}

Array.prototype.FullJoin = function (a, outerKey, innerKey, f) {
    outerKey = XFunc(outerKey);
    innerKey = XFunc(innerKey);
    f = XFunc(f);
    return this.SelectMany(function (x) {
        var ok = outerKey(x);
        if (ok !== undefined)
            return a.Select(function (y) {
                ik = innerKey(y);
                if (ok === ik)
                    return f.call(x, x, y);
                if (ik === undefined)
                    return f(undefined, y);
            });
        else
            return [f.call(x, x, undefined)];
    });
}

Array.prototype.GroupBy = function (f) {
    f = XFunc(f);
    var groups = [];
    var that = this;
    this.ForEach(function (x, i) {
        var key = f(x, i);
        var keyIndex = groups.FirstIndex(function (x) { return x.key === key });
        if (keyIndex === undefined) {
            var newItem = that.CloneWithoutItems();
            newItem.key = key;
            groups.push(newItem);
            keyIndex = groups.length - 1;
        }
        groups[keyIndex].push(x);
    });
    return groups;
}

Array.prototype.GroupJoin = function (a, outerKey, innerKey, f) {
    outerKey = XFunc(outerKey);
    innerKey = XFunc(innerKey);
    f = XFunc(f);
    return this.Select(function (x) {
        var ok = outerKey(x);
        if (ok !== undefined) {
            var y = a.Where(function (y) { return ok === innerKey(y); });
            if (y.Count())
                return f.call(x, x, y);
        }
    });
}

Array.prototype.Intersect = function (a) {
    return this.Where(function (x) { return a.Contains(x) });
}

Array.prototype.Join = function (a, outerKey, innerKey, f) {
    outerKey = XFunc(outerKey);
    innerKey = XFunc(innerKey);
    f = XFunc(f);
    return this.SelectMany(function (x) {
        var ok = outerKey(x);
        if (ok !== undefined)
            return a.Select(function (y) {
                if (ok === innerKey(y))
                    return f.call(x, x, y);
            });
    });
}

Array.prototype.Last = function (f) {
    return f ? this.Reverse().First(f) : this.ElementAt(this.Count() - 1);
}

Array.prototype.LeftGroupJoin = function (a, outerKey, innerKey, f) {
    outerKey = XFunc(outerKey);
    innerKey = XFunc(innerKey);
    f = XFunc(f);
    return this.Select(function (x) {
        var ok = outerKey(x);
        if (ok !== undefined) {
            var y = a.Where(function (y) { return ok === innerKey(y); });
            if (y.Count())
                return f.call(x, x, y);
            else
                return f.call(x, x, undefined);
        }
    });
}

Array.prototype.LeftJoin = function (a, outerKey, innerKey, f) {
    outerKey = XFunc(outerKey);
    innerKey = XFunc(innerKey);
    f = XFunc(f);
    return this.SelectMany(function (x) {
        var ok = outerKey(x);
        if (ok !== undefined)
            return a.Select(function (y) {
                if (ok === innerKey(y))
                    return f.call(x, x, y);
            });
        else
            return [f.call(x, x, undefined)];
    });
}

Array.prototype.Max = function (f) {
    if (f) return this.Where(f).Max();
    return this.Aggregate(this.First(), 'Math.max(x, y)');
}

Array.prototype.MaxIndex = function (f) {
    var max = f ? this.Max(f) : this.Max();
    return this.FirstIndex(function (x) { return x === max; });
}

Array.prototype.Min = function (f) {
    if (f) return this.Where(f).Min();
    return this.Aggregate(this.First(), 'Math.min(x, y)');
}

Array.prototype.MinIndex = function (f) {
    var min = f ? this.Min(f) : this.Min();
    return this.FirstIndex(function (x) { return x === min; });
}

Array.prototype.DoOrderBy = function (f) {
    f = XFunc(f);
    return this.sort(function (x, y) {
        var fx = f.call(x, x);
        var fy = f.call(y, y);
        return fx < fy ? -1 : (fx > fy ? 1 : 0);
    });
}

Array.prototype.OrderBy = function (f) {
    return this.Clone().DoOrderBy(f);
}

Array.prototype.DoOrderByDesc = function (f) {
    f = XFunc(f);
    return this.sort(function (x, y) {
        var fx = f.call(x, x);
        var fy = f.call(y, y);
        return fx < fy ? 1 : (fx > fy ? -1 : 0);
    });
}

Array.prototype.OrderByDesc = function (f) {
    return this.Clone().DoOrderByDesc(f);
}

Array.prototype.DoOrderByDescending = Array.prototype.DoOrderByDesc;
Array.prototype.OrderByDescending = Array.prototype.OrderByDesc;

Array.prototype.Range = function (from, to) {
    var result = to || to === 0 ? this.slice(from, to) : this.slice(from);
    for (var i in this)
        if (!/^\d+$/.test(i))
            result[i] = this[i];
    return result;
}

Array.prototype.DoRemove = function (from, count) {
    if (count !== 0) {
        if (arguments.length) {
            count = count || 1;
            for (i = from + count; i < this.length; i++)
                this[i - count] = this[i];
            this.length = this.length - count;
        } else
            this.length = 0;
	}
    return this;
}

Array.prototype.Remove = function (from, count) {
    if (count === 0 || !arguments.length) return this.CloneWithoutItems();
    return this.Take(from).Concat(this.Skip(from + (count || 1)));
}

Array.prototype.DoRemoveFirst = function (f) {
    return this.DoRemove(this.FirstIndex(f));
}

Array.prototype.DoRemoveItem = function (item) {
	while (this.Contains(item)) {
		this.DoRemoveFirst(function (x) { return x === item; });
	}
	return this;
}

Array.prototype.DoRemoveItems = function (items) {
	var that = this;
	items.ForEach(function () {
		that.DoRemoveItem(this);
	});
	return this;
}

Array.prototype.RemoveFirst = function (f) {
    return this.Remove(this.FirstIndex(f));
}

Array.prototype.DoRemoveLast = function (f) {
    return this.DoRemove(this.LastIndex(f));
}

Array.prototype.RemoveLast = function (f) {
    return this.Remove(this.LastIndex(f));
}

Array.prototype.DoRemoveWhere = function (f) {
    while (this.Any(f)) {
        return this.DoRemoveFirst(f);
    }
}

Array.prototype.DoReverse = function (f) {
    return this.reverse();
}

Array.prototype.Reverse = function (f) {
    return this.Clone().DoReverse();
}

Array.prototype.RightJoin = function (a, outerKey, innerKey, f) {
    outerKey = XFunc(outerKey);
    innerKey = XFunc(innerKey);
    f = XFunc(f);
    return this.SelectMany(function (x) {
        var ok = outerKey(x);
        if (ok !== undefined)
            return a.Select(function (y) {
                ik = innerKey(y);
                if (ok === ik)
                    return f.call(x, x, y);
                if (ik === undefined)
                    return f(undefined, y);
            });
    });
}

Array.prototype.Select = function (f) {
    f = XFunc(f);
    var result = [];
    this.ForEach(function (x, i) { var y = f.call(x, x, i); if (y !== undefined) result.push(y); });
    return result;
}

Array.prototype.SelectAll = function (f) {
    f = XFunc(f);
    var result = [];
    this.ForEach(function (x, i) { result.push(f.call(x, x, i)); });
    return result;
}

Array.prototype.SelectMany = function (f) {
    f = XFunc(f);
    var result = [];
    this.ForEach(function (x, i) { result.push.apply(result, f.call(x, x, i)); });
    return result;
}

Array.prototype.Single = function (x) {
    var s = this.Where(function (a) { return a === x; });
    return s.Count() == 1 ? x : undefined;
}

Array.prototype.SingleIndex = function (x) {
    var X = function (a) { return a === x; };
    var s = this.Where(X);
    return s.Count() == 1 ? this.FirstIndex(X) : undefined;
}

Array.prototype.Skip = function (countOrFunc) {
    if (typeof countOrFunc == 'string' || typeof countOrFunc == 'function') {
        var f = XFunc(countOrFunc);
        var i = this.FirstIndex(function (x) { return !f.call(x, x); });
        return this.Range(i);
    }
    return this.Range(countOrFunc);
}

Array.prototype.SkipLast = function (count) {
    return this.Range(0, -count);
}

Array.prototype.Sum = function (f) {
    if (f) return this.Where(f).Sum();
    return this.Aggregate(0, 'x + y');
}

Array.prototype.Take = function (countOrFunc) {
    if (typeof countOrFunc == 'string' || typeof countOrFunc == 'function') {
        var f = XFunc(countOrFunc);
        var i = this.FirstIndex(function (x) { return !f.call(x, x); });
        return this.Range(0, i);
    }
    return this.Range(0, countOrFunc);
}

Array.prototype.TakeLast = function (count) {
    return this.Range(count);
}

Array.prototype.Union = function (a) {
    return this.Concat(a.Except(this));
}

Array.prototype.Where = function (f) {
    var result = this.CloneWithoutItems();
    f = XFunc(f);
    this.ForEach(function (x, i) { if (f.call(x, x, i)) result.push(x); });
    return result;
}

Array.prototype.Zip = function (a, f) { // f is func(first, second) where first is item of this, and second is item of a
    f = XFunc(f);
    var t = a.Count() < this.Count() ? this.Take(a.Count()) : this;
    return t.Select(function (x, i) { return f.call(x, x, a.ElementAt(i)); });
}

range = function (from, to, step) {
	if (arguments.length < 2) { to = from; from = 0; }
	from = from || 0;
	to = to || 0;
	step = step || 1;
	var result = [];
	if (step > 0)
		for (var i = from; i < to; i += step)
			result.push(i);
	else
		for (var i = from; i > to; i += step)
			result.push(i);
	return result;
}

// TODO: ForEach, Select etc. for object (passing both key and value into inner func)
// TODO: Enumerable.Range with optional "from" and "step" parameters
// TODO: Working with Sets - that are objects with any values, where only keys have sense, hence could be used like: if (a in Set(1, 4, 5, 8)) { ... }