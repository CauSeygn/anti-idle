using Godot;
namespace AntiIdle.BattleArena.LootMechanics;

//TO DO attach to asset
// MATCH: DefineSprite_1312_newLoot11/frame_1/DoAction.as
public partial class NewLoot11 : Control
{
    private int i;
    private int yy;
    private int mm;
    private int dd;
    private double amntToGain;
    private double lootValue;
    private double x;
    private double y;
    private double _X;
    private double _Y;
    private double leftChance;
    private double magneticChance;
    private bool magnetic;
    private double yVel = -5;
    private double xalpha = 250;
    private double del = 0;
    private double xVel = Math.random() * 2;
    private double _xmouse;
    private double _ymouse;
    private double _alpha;

    // MATCH: DefineSprite_1312_newLoot11/frame_1/DoAction.as:getLoot()
    public void getLoot()
    {
        if (lootValue == 1)
        {
            _root.gainCareerEXP(4, 50, true);
            _root.save.arenaCrystal1 += 1;
            _root.dispNews(42, "Found 1 [Crystal of Rarity]!");
            _root.house.arena.showDamage("Crystal of Rarity +1", 13369086, _X, _Y - 20);
            if (_root.save.questType == "Loot")
            {
                if (_root.save.questSubtype == "Any" || _root.save.questSubtype == "Crystal of Rarity")
                {
                    _root.save.questCount += 1;
                }
            }
        }
        else if (lootValue == 2)
        {
            _root.gainCareerEXP(4, 200, true);
            _root.save.arenaCrystal2 += 1;
            _root.dispNews(43, "Found 1 [Crystal of Ultimate Rarity]!");
            _root.house.arena.showDamage("Crystal of Ultimate Rarity +1", 16698366, _X, _Y - 20);
            if (_root.save.questType == "Loot")
            {
                if (_root.save.questSubtype == "Any" || _root.save.questSubtype == "Crystal of Ultimate Rarity")
                {
                    _root.save.questCount += 1;
                }
            }
        }
    }

    public override void _Ready()
    {
        leftChance = 0.3;
        magneticChance = 1;
        _X = x;
        _Y = y - 50;
        xVel = Math.random() * 2;
        if (Math.random() < leftChance)
        {
            xVel = (-Math.random()) * 2;
        }
        magnetic = false;
        if (_root.lootMagnet == true && Math.random() < magneticChance)
        {
            magnetic = true;
        }
        if (_root.save.bouncyLoot == false)
        {
            if (magnetic == true)
            {
                _X = 80;
            }
            else if (_root.save.activityLoot == true && (_root.cursoridle < 5 || _root.arenaBot > 0 && _root.arenaBot < 2400))
            {
                _X = 80;
            }
            else if (x > 85)
            {
                _X = x + xVel * 100;
            }
            else
            {
                _X = x;
            }
            _Y = y;
        }
        yVel = -5;
        xalpha = 250;
        del = 0;
        gotoAndStop(lootValue);
    }

    public override void _Process(double delta)
    {
        del += 1;
        if (del >= 2)
        {
            del = 0;
            if (_Y > 0)
            {
                xVel *= 0.98;
                if (_root.save.activityLoot == true && (_root.cursoridle < 5 || _root.arenaBot > 0 && _root.arenaBot < 2400))
                {
                    xVel -= 1;
                    if (_root.save.bouncyLoot == false)
                    {
                        _X = 80;
                    }
                }
            }
            yVel += 1;
            if (_Y > 150 && yVel > 0)
            {
                yVel *= -0.6;
            }
            if (_root.save.bouncyLoot != false)
            {
                if (magnetic == true)
                {
                    xVel -= 1;
                }
                if (_Y > 0)
                {
                    _X = _X + xVel;
                }
                _Y = _Y + yVel;
            }
            if (_X > 500)
            {
                _X = 500;
            }
            if (xalpha > 0)
            {
                xalpha -= 100 / _root.fps;
                if (_X < 85 || _xmouse >= -25 && _xmouse <= 25 && _ymouse >= -50 && _ymouse <= 5 && _root.cursoridle < 60)
                {
                    _root.save.arenaLoot += 1;
                    getLoot();
                    QueueFree();
                }
            }
            else
            {
                QueueFree();
            }
            if (_root._quality == "HIGH" || _root._quality == "BEST")
            {
                _alpha = xalpha;
            }
        }
    }
}
