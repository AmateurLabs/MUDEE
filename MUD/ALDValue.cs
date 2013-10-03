
using System.Collections;

public struct ALDValue {
	private string _value;
	
	private ALDValue(string val) {
		_value = val;
	}
	
	public override string ToString ()
	{
		return _value;
	}
	
	public static explicit operator byte(ALDValue v) {
		return byte.Parse(v._value);
	}
	
	public static explicit operator ushort(ALDValue v) {
		return ushort.Parse(v._value);
	}
	
	public static explicit operator int(ALDValue v) {
		return int.Parse(v._value);
	}
	
	public static explicit operator float(ALDValue v) {
		return float.Parse(v._value);
	}
	
	public static explicit operator bool(ALDValue v) {
		return bool.Parse(v._value);
	}
	
	public static implicit operator string(ALDValue v) {
		return v._value;
	}
	
	public static implicit operator ALDValue(string v) {
		return new ALDValue(v);
	}
}