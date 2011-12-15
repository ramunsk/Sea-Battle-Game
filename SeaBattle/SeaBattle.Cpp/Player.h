#pragma once
#include "stdafx.h"
#include "Enums.h"
#include "ShootingEventArgs.h"

ref class Player abstract
{
    private:
    	bool _canSoot;
    
    protected:
	    Player(String^ name);
        initonly Dictionary<Point, ShotResult>^ PastShots;
	    void ShotTargetChosen(int x, int y);
        virtual void AddShotResult(int x, int y, ShotResult result);

    public:
	    property String^ Name;
	    virtual void Shoot();
	    virtual void Reset();
	    event EventHandler<ShootingEventArgs^>^ Shooting;
	    event EventHandler<ShootingEventArgs^>^ Shot;
	    event EventHandler^ MyTurn;
};
