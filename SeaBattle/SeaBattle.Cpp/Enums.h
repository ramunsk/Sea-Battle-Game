#pragma once

public enum struct ShotResult
{
	Missed,
	ShipHit,
	ShipDrowned
};

public enum struct BoardCellState
{
	Normal,
	MissedShot,
	Ship,
	ShotShip,
	ShipDrag,
	ShipDragInvalid,
	ShowDrowned
};

public enum struct ShipOrientation
{
	Horizontal,
	Vertical
};

public enum struct BoardMode
{
    Design,
    Game
};