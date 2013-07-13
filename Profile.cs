package NW_ProfileCreation
{
  function GameConnection::NW_createProfile(%this,%check)
	{
		if(isObject(%this))
		{
			if(%this.isAdmin)
			{
				%admincheck = 1;
				warn("NW Profile Creation - Client is admin.");
			}
			%filepath = "config/server/National Warfare/Profiles/"@%this.getBLID()@"/career.txt";
			if(isFile(%filepath))
			{
				if(%check == 1)
				{
					fileDelete(%filepath);
					%this.NW_createProfile(0);
				}
				else
				{
					return;
				}
			}
			else
			{
				commandToClient(%this,'NW_CheckPlayerID');
				%this.level = 1;
				%this.rank = 0;
				%this.exp = 0;
				%this.neededexp = %this.level * 50 * 5 * 15 * %this.level;
				%this.setKills(0);
				%this.setDeaths(0);
				%this.setWins(0);
				%this.setLosses(0);
				%file = new FileObject();
				%file.openForWrite(%filepath);
				%file.writeLine("Name: "@%this.name);
				%file.writeLine("BLID: "@%this.getBLID());
				%file.writeLine("PLID: "@%this.pl_id);
				%file.writeLine("Level: "@%this.level);
				%file.writeLine("Rank: "@%this.rank);
				%file.writeLine("EXP: "@%this.exp);
				%file.writeLine("Needed EXP: "@%this.neededexp);
				%file.writeLine("Kills: "@%this.getKills());
				%file.writeLine("Deaths: "@%this.getDeaths());
				%file.writeLine("Wins: "@%this.getWins());
				%file.writeLine("Losses: "@%this.getLosses());
				%file.close();
				%file.delete();
				NW_NotifyPlayer(%this,"Your profile has been created, thank you and enjoy this server.","Profile Creation Notification");
			}
		}
	}
};
activatePackage(NW_ProfileCreation);
