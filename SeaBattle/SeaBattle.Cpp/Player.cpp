#include "stdafx.h"
#include "Player.h"

Player::Player(String^ name)
{
	Name = name;
	PastShots = gcnew Dictionary<Point, ShotResult>();
};

void Player::ShotTargetChosen(int x, int y)
{
	if (!_canSoot)
		return;

	_canSoot = false;

	ShootingEventArgs^ eventArgs = gcnew ShootingEventArgs(x, y);
	Shooting(this, eventArgs);
	AddShotResult(x, y, eventArgs->Result);

	Shot(this, eventArgs);
};

void Player::AddShotResult(int x, int y, ShotResult result)
{
	PastShots[Point(x, y)] = result;
}

void Player::Shoot()
{
	_canSoot = true;

    MyTurn(this, gcnew EventArgs());
}

void Player::Reset()
{
	PastShots->Clear();
	_canSoot = false;
}



