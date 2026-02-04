<p style="text-align: center;"><span style="color: #e74c3c;">Quick Note: V1.0.2 had some bugs that would have meant the mod wasn't functioning as intended, please update to v1.3 as soon as you can (sorry :()</span></p>
<h2 style="text-align: left;">&nbsp;</h2>
<h2 style="text-align: left;"><em>Intro</em></h2>
<hr />
<p style="text-align: left;">&nbsp;</p>
<p style="text-align: left;">This mod introduces a slightly more nuanced hunger system into the game without overcomplicating gameplay or slipping into simulation territory. The goal was to aim for compatibly with as many things as possible (stuff like ExpandedFoods etc) so it tries to work with the vanilla systems.</p>
<p style="text-align: left;">Instead of damage at 0 saturation:</p>
<ul>
<li style="text-align: left;">The saturation bar now acts as your stomach contents</li>
<li style="text-align: left;">As saturation decreases food is digested and converted into body weight to act as a store of saturation.</li>
<li style="text-align: left;">As your weight changes a configurable list of buffs and debuffs are applied to your character.</li>
</ul>
<p style="text-align: left;">Don't eat too much in one go or you'll spew and empty your stomach contents all over the floor, losing your current saturation.</p>
<p style="text-align: left;">You can see the current bonuses and penalties can be seen in the character panel ('C' by default).</p>
<h2 style="text-align: left;"><em>What This Mod Does</em></h2>
<hr />
<p>&nbsp;</p>
<ul>
<li>Removes hunger damage when saturation reaches 0</li>
<li>Removes saturation loss delay after eating</li>
<li>Stops saturation being a factor in calculating health regen</li>
<li>Allows eating past Max Saturation</li>
<li>Forces full consumption of meals so no partial portions</li>
<li>Tracks eaten saturation and converts it to body weight</li>
<li>Adds buffs/debuffs depending on the current body weight</li>
<li>Burns body weight over time, factoring in
<ul>
<li>Sprinting</li>
<li>Sleeping</li>
<li>Standing still</li>
</ul>
</li>
<li>(Configurable) Inflicts lethal damage when the critical weight is reached</li>
</ul>
<h2 style="text-align: left;"><em>What This Mod Doesn't Do</em></h2>
<hr />
<p>&nbsp;</p>
<ul>
<li>Touch anything to do with nutrition (still applies health bonuses with a varied diet)</li>
<li>Change any saturation values&nbsp;</li>
</ul>
<h2 style="text-align: left;"><em>Bonuses and Penalties</em></h2>
<hr />
<p>&nbsp;</p>
<p style="text-align: left;">Here are the default bonuses and penalties:</p>
<div class="spoiler">
<div class="spoiler-toggle">Default buffs/debuffs</div>
<div class="spoiler-text">
<table dir="ltr" style="table-layout: fixed; font-size: 10pt; font-family: Arial; width: 0px; border-collapse: collapse; border: medium; height: 189px; border-spacing: 0px;" border="1" cellpadding="0" data-sheets-root="1" data-sheets-baot="1"><colgroup><col width="100" /><col width="100" /><col width="86" /><col width="71" /><col width="94" /><col width="105" /><col width="132" /><col width="79" /><col width="151" /><col width="149" /><col width="287" /><col width="252" /><col width="115" /><col width="86" /><col width="108" /><col width="100" /></colgroup>
<tbody>
<tr style="height: 21px;">
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 100px;">Weight</td>
<td style="overflow: hidden; padding: 2px 3px; font-family: JetBrains Mono; font-size: 9pt; font-weight: bold; height: 21px; width: 100px; vertical-align: bottom;">WalkSpeed</td>
<td style="overflow: hidden; padding: 2px 3px; font-family: JetBrains Mono; font-size: 9pt; font-weight: bold; height: 21px; width: 86px; vertical-align: bottom;">MiningSpeed</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 71px;">MaxHealth</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 94px;">MeleeDamage</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 105px;">RangedDamage</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 132px;">HealingEffectiveness</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 79px;">HungerRate</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 151px;">RangeWeaponAccuracy</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 149px;">RangedWeaponsSpeed</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 287px;">RustyGearDropRate</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 252px;">AnimalSeekingRange</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 115px;">BowDrawStrength</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 86px;">GliderLiftMax</td>
<td style="overflow: hidden; padding: 2px 3px; font-family: JetBrains Mono; font-size: 9pt; font-weight: bold; height: 21px; width: 108px; vertical-align: bottom;">GliderSpeedMax</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 100px;">JumpHeightMul</td>
</tr>
<tr style="height: 21px;">
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 100px;">40</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 100px;">-0.3</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 86px;">-0.25</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 71px;">-3</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 94px;">-0.5</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 105px;">-0.3</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 132px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 79px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 151px;">-0.6</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 149px;">-0.6</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 287px;">0.3</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 252px;">-0.3</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 115px;">-0.6</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 86px;">0.3</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 108px;">0.3</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 100px;">&nbsp;</td>
</tr>
<tr style="height: 21px;">
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 100px;">50</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 100px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 86px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 71px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 94px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 105px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 132px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 79px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 151px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 149px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 287px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 252px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 115px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 86px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 108px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 100px;">&nbsp;</td>
</tr>
<tr style="height: 21px;">
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 100px;">60</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 100px;">0</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 86px;">0</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 71px;">0</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 94px;">0</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 105px;">0</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 132px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 79px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 151px;">0.1</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 149px;">0</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 287px;">0</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 252px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 115px;">-0.2</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 86px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 108px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 100px;">&nbsp;</td>
</tr>
<tr style="height: 21px;">
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 100px;">70</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 100px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 86px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 71px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 94px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 105px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 132px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 79px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 151px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 149px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 287px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 252px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 115px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 86px;">0</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 108px;">0</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 100px;">&nbsp;</td>
</tr>
<tr style="height: 21px;">
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 100px;">80</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 100px;">0.2</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 86px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 71px;">5</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 94px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 105px;">0.3</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 132px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 79px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 151px;">0.3</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 149px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 287px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 252px;">0</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 115px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 86px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 108px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 100px;">&nbsp;</td>
</tr>
<tr style="height: 21px;">
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 100px;">90</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 100px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 86px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 71px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 94px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 105px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 132px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 79px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 151px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 149px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 287px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 252px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 115px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 86px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 108px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 100px;">&nbsp;</td>
</tr>
<tr style="height: 21px;">
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 100px;">100</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 100px;">-0.2</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 86px;">0.3</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 71px;">3</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 94px;">0.3</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 105px;">0.1</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 132px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 79px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 151px;">0.1</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 149px;">0.2</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 287px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 252px;">0.3</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 115px;">0.2</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 86px;">-0.5</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; text-align: right; height: 21px; width: 108px;">-0.5</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 100px;">&nbsp;</td>
</tr>
<tr style="height: 21px;">
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 100px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 100px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 86px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 71px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 94px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 105px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 132px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 79px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 151px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 149px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 287px;">//lucks gotta turn around at some point right???</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 252px;">//animals will find the extra fat extra tasty</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 115px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 86px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 108px;">&nbsp;</td>
<td style="overflow: hidden; padding: 2px 3px; vertical-align: bottom; height: 21px; width: 100px;">&nbsp;</td>
</tr>
</tbody>
</table>
</div>
</div>
<p>&nbsp;</p>
<p>The value of the buff is linearly interpolated between points. If it's the first in the list then it'll assume a 0 at the critical weight and fade in from there but if it's the last weight in the list it'll stick at the max value unless you add buff with a value of 0 at the MaxWeight.</p>
<p>For example imagine you've created a custom list that has a value of 1 at 60kg and a value of 2 at 80kg the values would look like this:</p>
<p style="padding-left: 40px;">When player weight is 40kg then the value would be 0</p>
<p style="padding-left: 40px;">When player weight is 50kg then the value would be 0.5</p>
<p style="padding-left: 40px;">When player weight is 60kg then the value would be 1</p>
<p style="padding-left: 40px;">When player weight is 70kg then the value would be 1.5</p>
<p style="padding-left: 40px;">When player weight is 80kg and above then the value would be 2</p>
<h2 style="text-align: left;"><em>Commands</em></h2>
<hr />
<p>&nbsp;</p>
<p>There are some debug commands you can use if things go awry or you want to do some testing:</p>
<p>/weight set &lt;weight in kg&gt;</p>
<p>/weight playerset &lt;player name&gt; &lt;weight in kg&gt;</p>
<p>/weight clearaveragegain //clears the average gain tracker back to 0 if it gets weird</p>
<p>&nbsp;</p>
<h2 style="text-align: left;"><em>Configuration</em></h2>
<hr />
<p>&nbsp;</p>
<p>This mod is highly configurable and will need some tweaking initially - the intention is to find a sweetspot after a few people have tried it 😎</p>
<p>Here are the properties you can change along with their defaults and what they do:</p>
<div class="spoiler">
<div class="spoiler-toggle">Configurable Properties</div>
<div class="spoiler-text">
<ul>
<li>HealthyWeight (75) - Used to calculate how long a healthy person would take to starve using ExpectedSaturationPerDay and NumberOfMonthsToStarve.</li>
<li>CriticalWeight (40) - The lowest weight you can be before you suffer organ failure and damage starts being applied.</li>
<li>MaxWeight (100) - The maximum weight you can be.</li>
<li>ExpectedSaturationPerDay (4000) - Roughly how much saturation per day you'd expect to eat to maintain your weight.</li>
<li>NumberOfMonthsToStarve (1.5) - Roughly how long it would take to starve from HealthyWeight to CriticalWeight. This is then used to calculate how much saturation that would be like this:
<pre class="language-markup"><code>ExpectedSaturationPerDay * World.Calendar.DaysPerMonth (world gen days per month setting) * NumberOfMonthsToStarve;​</code></pre>
</li>
<li>ThrowUpThreshold (250) - How much saturation you can eat over the max saturation before you vomit.</li>
<li>ApplyWeightBonuses (true) - Whether or not the mod will apply weight bonuses.</li>
<li>WeightLossOnDeath (50) - What percentage of your current weight that you'll lose on death (100kg -&gt; 70kg etc).</li>
<li>LowestPossibleWeightOnRespawn (45) - A cap on how low you can go when you die and lose bodyweight.</li>
<li>PlayerStartingWeight (60) - The weight that the player spawns in with.</li>
<li>WeightBonuses - The bonuses applied to the player. This one is a little more complicated and I'll explain in the next section.</li>
<li>StoodStillModifier (0.25) - How much less energy the player burns when standing still. By default 4 times less.</li>
<li>SleepModifier (0.25) - How much less energy the player burns when sleeping. By default 4 times less.</li>
<li>SprintModifier (0.1) - How much more energy the player burns when sprinting. By default 10% more.</li>
<li>AlwaysConsumeFullMeal (true) - Whether or not the player will always eat the full meal or leave portions.</li>
<li>ApplyFatalDamageOnCriticalWeight (true) - Whether the player can die from hunger.</li>
<li>AverageGainCheckWindowInHours (12) - Used to tune the average weight gain check. How many hours the average weight gain check is based on.</li>
<li>AverageGainCheckFrequencyInHours (1) - Used to tune the average weight gain check. How long between the average weight checks.</li>
</ul>
</div>
</div>
<h2 style="text-align: left;"><em>Updating the Bonuses</em></h2>
<hr />
<p>&nbsp;</p>
<p>The bonuses that are applied are also configurable and the GUI should automatically update to accomodate them. The bonus types past 14 are not properly supported but I may add them if there's interest. Here are the values for each bonus that can be applied:</p>
<p>&nbsp;</p>
<div class="spoiler">
<div class="spoiler-toggle">Bonus Types</div>
<div class="spoiler-text">
<ul>
<li>WalkSpeed = 0</li>
<li>MiningSpeed = 1</li>
<li>MaxHealth = 2</li>
<li>MeleeDamage = 3</li>
<li>RangedDamage = 4</li>
<li>HealingEffectiveness = 5</li>
<li>HungerRate = 6</li>
<li>RangeWeaponAccuracy = 7</li>
<li>RangedWeaponsSpeed = 8</li>
<li>RustyGearDropRate = 9</li>
<li>AnimalSeekingRange = 10</li>
<li>BowDrawStrength = 11</li>
<li>GliderLiftMax = 12</li>
<li>GliderSpeedMax = 13</li>
<li>JumpHeightMul = 14</li>
</ul>
<p>&nbsp;</p>
<p>&nbsp;--Not supported but might still work--</p>
<ul>
<li>WholeVesselLootChance = 15</li>
<li>TemporalGearRepairCost = 16</li>
<li>AnimalHarvestTime = 17</li>
<li>MechanicalsDamage = 18</li>
<li>AnimalLootDropRate = 19</li>
<li>ForageDropRate = 20</li>
<li>WildCropDropRate = 21</li>
<li>VesselContentsDropRate = 22</li>
<li>OreDropRate = 23</li>
<li>ArmorWalkSpeed = 24</li>
<li>ArmorDurabilityLoss = 25</li>
</ul>
</div>
</div>
<p>&nbsp;</p>
<p>You then choose the bonus type, value and weight to be applied at and add it to WeightBonuses. Check the default config if you're not used to JSON</p>
<p>e.g A 30% boost to melee damage at 65kg</p>
<pre class="language-markup"><code>{
     "Weight": 65.0,
     "Type": 3,
     "Value": 0.3
},</code></pre>
<h2 style="text-align: left;"><em>Incompatibilities</em></h2>
<hr />
<p>&nbsp;</p>
<p><span style="text-decoration: line-through;">Currently soft incompatbility with Hydrate or Diedrate as it overwrites OnEntityReceiveSaturation which means that you're unable to eat past max sat and vomit. Currently working on it though as I'd quite like to use them together personally :)</span> - Has now been updated to improve the way they handle OnEntityReceiveSaturation so should be compatible now. I'll be doing some testing soon to make sure everything works.</p>
<p>Please let me know if you find anything else 🕺</p>
