namespace ChessGame
{
	public class ChessPosition
	{
		public int XCoord { get; private set; }
		public int YCoord { get; private set; }

		public ChessPosition(int xCoord, int yCoord)
		{
			XCoord = xCoord;
			YCoord = yCoord;
		}

		public override string ToString()
		{
			return $"{(char)('a' + XCoord)}{YCoord + 1}";
		}

		public override bool Equals(object obj)
		{
			if (obj is ChessPosition other)
			{
				return XCoord == other.XCoord && YCoord == other.YCoord;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (XCoord, YCoord).GetHashCode();
		}

		public static ChessPosition FromString(string position)
		{
			return new ChessPosition(position[0] - 'a', int.Parse(position.Substring(1)) - 1);
		}
	}

}
