namespace ChessGame
{
	public class MoveCommand
	{
		public ChessPosition Src { get; private set; }
		public ChessPosition Dst { get; private set; }

		public MoveCommand(ChessPosition src, ChessPosition dst)
		{
			Src = src;
			Dst = dst;
		}

		public static MoveCommand FromString(string command)
		{
			var tokens = command.Split(' ');
			if (tokens.Length != 2) return null;
			var src = ChessPosition.FromString(tokens[0]);
			var dst = ChessPosition.FromString(tokens[1]);
			return new MoveCommand(src, dst);
		}
	}

}
