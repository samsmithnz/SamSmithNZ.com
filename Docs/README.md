## Process to add a new tournament to world cup

1. Tournament dates and format decided 
    1. Create tournament + adding details + round formats (not automated)
    2. Add playoff format (only automated for existing tournaments)
2. Qualitification complete 
    1. Add teams, ELO ratings, etc (Adding teams as tool assistance. ELO ratings manual)
    2. Setup teams in groups (group arranging has tool assistance)
    3. Add games (automation completed)
3. Tournament start imminent
    1. Add players for teams (Automation created)
4. Tournament started
    1. Add scores/goal scorers, etc (automation created. Playoff games need minor assistance + checking)

## Odds Calculation Feature

The odds calculation feature provides a module for calculating the odds of all teams to win in a tournament. This module leverages existing team and game data structures, along with ELO calculations for individual games, to calculate the odds. It includes functions to calculate individual game outcomes based on team ELO ratings and historical performance. The odds calculation module integrates with the tournament setup process, providing pre-tournament and dynamic in-tournament odds updates.

To access odds data through the web interface, navigate to the "Odds" section of the World Cup tournament page. For API access, use the `GetOddsForTournament` method available in the `OddsController`.
