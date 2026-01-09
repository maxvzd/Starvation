<h3 style="text-align: center;"><strong><span style="color: #e74c3c;">BEFORE INSTALLING PLEASE READ THIS!!!</span></strong></h3>
<p style="text-align: center;"><strong><span style="color: #e74c3c;">This mod has not yet been tested across a full playthrough. Expect bugs, balance issues, and features that may not behave as intended.</span></strong></p>
<p style="text-align: center;"><strong><span style="color: #e74c3c;">It should be safe to install or uninstall mid-playthrough.</span></strong></p>
<p style="text-align: center;"><strong><span style="color: #e74c3c;">Multiplayer has not yet been tested and may not work correctly.</span></strong></p>
<p style="text-align: center;"><strong><span style="color: #e74c3c;">If you try the mod, please report any issues you encounter using the issue tracker above.</span></strong></p>
<p style="text-align: center;"><strong><span style="color: #e74c3c;">ty</span></strong></p>
<h3 style="text-align: center;"><em>Intro</em></h3>
<p style="text-align: left;">This mod introduces a slightly more nuanced hunger system into the game without overcomplicating gameplay or slipping into simulation territory. The goal was to aim for compatibly with as many things as possible (stuff like ExpandedFoods etc) so it tries to work with the vanilla systems.</p>
<p style="text-align: left;">Instead of damage at 0 saturation:</p>
<ul>
<li style="text-align: left;">The saturation bar now acts as your stomach contents</li>
<li style="text-align: left;">As saturation decreases food is digested and converted into body weight to act as a store of saturation.</li>
<li style="text-align: left;">As your weight changes a configurable list of buffs and debuffs are applied to your character.</li>
</ul>
<p style="text-align: left;">Don't eat too much in one go or you'll spew and empty your stomach contents all over the floor, losing your current saturation.</p>
<p style="text-align: left;">You can see the current bonuses and penalties can be seen in the character panel ('C' by default).</p>
<h3 style="text-align: center;"><em>What This Mod Does</em></h3>
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
</ul>
<h3 style="text-align: center;"><em>What This Mod Doesn't Do</em></h3>
<ul>
<li>Touch anything to do with nutrition (still applies health bonuses with a varied diet)</li>
<li>Change any saturation values&nbsp;</li>
</ul>
<h3 style="text-align: center;"><em>Configuration</em></h3>
<p>This mod is highly configurable and will need some tweaking initially - the intention is to find a sweetspot after a few people have tried it 😎</p>
<p>Here are the properties you can change along with their defaults and what they do:</p>
<div class="spoiler">
<div class="spoiler-toggle">Configurable Properties</div>
<div class="spoiler-text">
<ul>
<li>HealthyWeight (75) - Used to calculate how long a health person would take to starve usingExpectedSaturationPerDay and NumberOfMonthsToStarve</li>
<li>CriticalWeight (40) - The lowest weight you can be before damage starts being applied</li>
<li>MaxWeight (100) - The maximum weight you can be</li>
<li>ExpectedSaturationPerDay (4000) - Roughly how much sat per day you'd expect to eat</li>
<li>NumberOfMonthsToStarve (1.5) - Roughly how long it would take to starve consuming the ExpectedSaturationPerDay from HealthyWeight</li>
<li>ThrowUpThreshold (250) - How much saturation you can eat over the max saturation before you vomit</li>
<li>ApplyWeightBonuses (true) - Whether or not the mod will apply weight bonuses</li>
<li>WeightLossOnDeath (50) - What percentage of your current weight that you'll lose on death (100kg -&gt; 70kg etc)</li>
<li>LowestPossibleWeightOnRespawn (45) - A cap on how low you can go when you die and lose bodyweight</li>
<li>PlayerStartingWeight (60) - The weight that the player spawns in with&nbsp;</li>
<li>WeightBonuses - This one is a little more complicated and I'll explain in the next section</li>
<li>StoodStillModifier (0.25) - How much less energy the player burns when standing still. By default 4 times less</li>
<li>SleepModifier (0.25) - How much less energy the player burns when sleeping. By default 4 times less</li>
<li>SprintModifier (0.1) - How much more energy the player burns when sprinting. By default 10% more.</li>
</ul>
</div>
</div>
<h3 style="text-align: center;"><em>Updating the bonuses</em></h3>
<p>The bonuses that are applied are also configurable and the GUI should automatically update to accomodate them. The bonys types past 14 are not properly supported but I may add them if there's interest. Here are the values for each bonus that can be applied:</p>
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
<p>You then choose the bonus type, value and weight to be applied at and add it to WeightBonuses. Check the default config if you're not used to JSON</p>
<p>e.g A 30% boost to melee damage at 65kg</p>
<pre class="language-markup"><code>{
     "Weight": 65.0,
     "Type": 3,
     "Value": 0.3
},</code></pre>
<div class="spoiler">
<div class="spoiler-toggle">Original Default buffs/debuffs</div>
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
