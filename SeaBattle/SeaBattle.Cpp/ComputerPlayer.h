#pragma once
#include "stdafx.h"
#include "Player.h"

ref class ComputerPlayer : Player
{
	private:
		initonly Random^ _rnd;
		initonly Timer^ _timer;
		initonly List<Point>^ _currentTarget;
        void OnTimer(Object^ sender, EventArgs^ e);
        void ShootRandom();
		bool IsValidShot(Point p);
		Point GetRandomNeighbour(Point p);
        void TryDownShip();
		void ShipDrowned();

	public:
		ComputerPlayer(String^ name);
		virtual void Shoot() override;
        virtual void Reset() override;

	protected:
		virtual void AddShotResult(int x, int y, ShotResult result) override;
};
