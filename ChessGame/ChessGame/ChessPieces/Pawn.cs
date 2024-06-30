namespace ChessGame.ChessPieces
{
    public class Pawn : Piece
    {
        public Pawn(ChessPosition position, string color) : base(position, color) { }

        public override List<ChessPosition> GetThreatenedPositions(ChessBoard board)
        {
            var threatenedPositions = new List<ChessPosition>();
            var direction = Color == WHITE ? 1 : -1;

            var threatPositionLeft = board.SpotSearchThreat(Position, Color, -1, direction, true);
            if (threatPositionLeft != null) threatenedPositions.Add(threatPositionLeft);

            var threatPositionRight = board.SpotSearchThreat(Position, Color, 1, direction, true);
            if (threatPositionRight != null) threatenedPositions.Add(threatPositionRight);

            return threatenedPositions;
        }

        public override List<ChessPosition> GetMoveablePositions(ChessBoard board)
        {
            var moveablePositions = new List<ChessPosition>();
            var direction = Color == WHITE ? 1 : -1;

            var movePosition = board.SpotSearchThreat(Position, Color, 0, direction);
            if (movePosition != null) moveablePositions.Add(movePosition);

            if (Color == WHITE && Position.YCoord == 1 || Color == BLACK && Position.YCoord == 6)
            {
                var movePositionDouble = board.SpotSearchThreat(Position, Color, 0, 2 * direction);
                if (movePositionDouble != null) moveablePositions.Add(movePositionDouble);
            }

            return moveablePositions;
        }

        protected override string SymbolImpl() => "Pa";
    }

}
