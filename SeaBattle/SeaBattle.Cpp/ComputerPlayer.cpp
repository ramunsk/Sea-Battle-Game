#include "stdafx.h"
#include "ComputerPlayer.h"
#include "Rect.h"


ComputerPlayer::ComputerPlayer(String^ name) : Player(name)
{
	_rnd = gcnew Random(DateTime::Now.Millisecond);
	_currentTarget = gcnew List<Point>();
	_timer = gcnew Timer;
    _timer->Enabled = false;
	_timer->Tick += gcnew EventHandler(this, &ComputerPlayer::OnTimer);
}

void ComputerPlayer::OnTimer(Object^ sender, EventArgs^ e)
{
	_timer->Stop();
	if (_currentTarget->Count == 0)
	{
		ShootRandom();
		return;
	}

	TryDownShip();
};

void ComputerPlayer::ShootRandom()
{
	int x;
	int y;
	do
	{
		x = _rnd->Next(0, 10);
		y = _rnd->Next(0, 10);
	} while (PastShots->ContainsKey(Point(x, y)));

	ShotTargetChosen(x, y);
};

bool ComputerPlayer::IsValidShot(Point p)
{
	return !PastShots->ContainsKey(p) && (gcnew Rect(0, 0, 10, 10))->Contains(p);
};

Point ComputerPlayer::GetRandomNeighbour(Point p)
{
	int x;
	int y;
	do
	{
		x = _rnd->Next(-1, 2);
		y = _rnd->Next(-1, 2);
	} while (Math::Abs(x + y) != 1);

	return Point(p.X + x, p.Y + y);
};

void ComputerPlayer::TryDownShip()
{
	Point lastHit;
	Point prevHit;
	Point nextShot;

	if (_currentTarget->Count == 1)
	{
		lastHit = _currentTarget[0];

		do
		{
			nextShot = GetRandomNeighbour(lastHit);
		} while (!IsValidShot(nextShot));
	}
	else
	{
		lastHit = _currentTarget[_currentTarget->Count - 1];
		prevHit = _currentTarget[_currentTarget->Count - 2];

		int x = lastHit.X - prevHit.X;
		int y = lastHit.Y - prevHit.Y;

		nextShot = Point(lastHit.X + x, lastHit.Y + y);

		if (!IsValidShot(nextShot))
		{
			x = _currentTarget[0].X - _currentTarget[1].X;
			y = _currentTarget[0].Y - _currentTarget[1].Y;

			nextShot = Point(_currentTarget[0].X + x, _currentTarget[0].Y + y);

			if (!IsValidShot(nextShot))
				throw gcnew Exception("Your logic just failed");
		}
	}

	ShotTargetChosen(nextShot.X, nextShot.Y);
};

void ComputerPlayer::ShipDrowned()
{
	for each (Point p in _currentTarget)
	{
		PastShots[Point(p.X - 1, p.Y - 1)] = ShotResult::ShipDrowned;
		PastShots[Point(p.X - 1, p.Y)] = ShotResult::ShipDrowned;
		PastShots[Point(p.X - 1, p.Y + 1)] = ShotResult::ShipDrowned;

		PastShots[Point(p.X, p.Y - 1)] = ShotResult::ShipDrowned;
		PastShots[Point(p.X, p.Y)] = ShotResult::ShipDrowned;
		PastShots[Point(p.X, p.Y + 1)] = ShotResult::ShipDrowned;

		PastShots[Point(p.X + 1, p.Y - 1)] = ShotResult::ShipDrowned;
		PastShots[Point(p.X + 1, p.Y)] = ShotResult::ShipDrowned;
		PastShots[Point(p.X + 1, p.Y + 1)] = ShotResult::ShipDrowned;
	}

	_currentTarget->Clear();
};

void ComputerPlayer::Shoot()
{
	Player::Shoot();
	_timer->Interval = _rnd->Next(100, 1000);
	_timer->Start();
}

void ComputerPlayer::AddShotResult(int x, int y, ShotResult result)
{
	Player::AddShotResult(x, y, result);
	if (result == ShotResult::ShipDrowned)
	{
		_currentTarget->Add(Point(x, y));
		ShipDrowned();
		return;
	}

	if (result == ShotResult::ShipHit)
	{
		_currentTarget->Add(Point(x, y));
	}

}

void ComputerPlayer::Reset()
{
	Player::Reset();
	_currentTarget->Clear();
}
