# NKL Klassenlotterie-Simulator
Dieser Klassenlotterie-Simulator spielt hunderte und tausende NKL-Saisons mit einer Losnummer in kurzer Zeit durch 
und hilft bei der Betrachtung von Zulosungswahrscheinlichkeiten realer Gewinne analog dem Gewinnplan der NKL.

__Es handelt sich bei dieser Software um eine reine Simulation. Ausgewiesene Gewinne sind nicht real.__

Momentan sind Gewinne und Lospreise für die 139. NKL-Lotterie in dieser Software hinterlegt. 

### Wie geht's

Öffne die _Program.cs_-File und ändere passe ggf. die Parameter an (Losnummer, Losanteil, Millionen-Joker und Renten-Joker etc.) sowie die Anzahl der durchzuführenden Simulationen. Führe das Programm aus und verfolge die Verlosungen in der Konsolenausgabe. Zum Ende wirst du eine
Gesamtauswertung zu allen Gewinnen und eine Gegenüberstellung zu den Einsätzen bekommen.

### Beispiel-Ausgabe mit 100 Simulationen

Ein Punkt entspricht einer Klasse (es gibt stets sechs Klassen pro NKL-Saison). 
Kam es zu einem Gewinn mit der eingestellten Losnummer, wird dieser angezeigt. 
Es werden nicht nur Geld- sondern auch Sachgewinne dargestellt. 

1. Runde:..... (0 Euro)
2. Runde:..... (0 Euro)
3. Runde:..... (0 Euro)
4. Runde:.....960  (960 Euro)
5. Runde:..... (960 Euro)
6. Runde:.160 .... (1120 Euro)
7. Runde:..... (1120 Euro)
8. Runde:..... (1120 Euro)
9. Runde:..... (1120 Euro)
10. Runde:..... (1120 Euro)
11. Runde:.....960  (2080 Euro)
12. Runde:..... (2080 Euro)
13. Runde:..... (2080 Euro)
14. Runde:..... (2080 Euro)
15. Runde:...160 .160 .960  (3360 Euro)
16. Runde:....160 . (3520 Euro)
17. Runde:.160 .... (3680 Euro)
18. Runde:...160 .. (3840 Euro)
19. Runde:...160 .. (4000 Euro)
20. Runde:.160 .... (4160 Euro)
21. Runde:..... (4160 Euro)
22. Runde:..160 ... (4320 Euro)
23. Runde:.....960  (5280 Euro)
24. Runde:.....960  (6240 Euro)
25. Runde:.160 .160 ... (6560 Euro)
26. Runde:..... (6560 Euro)
27. Runde:..... (6560 Euro)
28. Runde:.160 .... (6720 Euro)
29. Runde:.....960  (7680 Euro)
30. Runde:.....960  (8640 Euro)
31. Runde:...10k .. (18640 Euro)
32. Runde:.....960  (19600 Euro)
33. Runde:..... (19600 Euro)
34. Runde:.....960  (20560 Euro)
35. Runde:..... (20560 Euro)
36. Runde:...160 .. (20720 Euro)
37. Runde:..... (20720 Euro)
38. Runde:..... (20720 Euro)
39. Runde:..1k ...960  (22680 Euro)
40. Runde:..... (22680 Euro)
41. Runde:.....960  (23640 Euro)
42. Runde:..160 ... (23800 Euro)
43. Runde:..160 ... (23960 Euro)
44. Runde:.160 .... (24120 Euro)
45. Runde:..... (24120 Euro)
46. Runde:..... (24120 Euro)
47. Runde:..... (24120 Euro)
48. Runde:..... (24120 Euro)
49. Runde:..... (24120 Euro)
50. Runde:.....960  (25080 Euro)
51. Runde:....160 . (25240 Euro)
52. Runde:160 .....960  (26360 Euro)
53. Runde:..... (26360 Euro)
54. Runde:.....960  (27320 Euro)
55. Runde:..... (27320 Euro)
56. Runde:..... (27320 Euro)
57. Runde:..... (27320 Euro)
58. Runde:.....960  (28280 Euro)
59. Runde:..160 ..160 . (28600 Euro)
60. Runde:.....960  (29560 Euro)
61. Runde:....160 . (29720 Euro)
62. Runde:..160 ... (29880 Euro)
63. Runde:....160 . (30040 Euro)
64. Runde:..... (30040 Euro)
65. Runde:..... (30040 Euro)
66. Runde:..... (30040 Euro)
67. Runde:160 ..... (30200 Euro)
68. Runde:.....960  (31160 Euro)
69. Runde:.160 ...160 .960  (32440 Euro)
70. Runde:..... (32440 Euro)
71. Runde:..... (32440 Euro)
72. Runde:...160 ..960  (33560 Euro)
73. Runde:..... (33560 Euro)
74. Runde:..... (33560 Euro)
75. Runde:.....960  (34520 Euro)
76. Runde:.....960  (35480 Euro)
77. Runde:..160 ...960  (36600 Euro)
78. Runde:.....960  (37560 Euro)
79. Runde:..160 ... (37720 Euro)
80. Runde:.....960  (38680 Euro)
81. Runde:.160 .160 ...960  (39960 Euro)
82. Runde:...160 .. (40120 Euro)
83. Runde:...160 .. (40280 Euro)
84. Runde:..... (40280 Euro)
85. Runde:..... (40280 Euro)
86. Runde:..160 ...960  (41400 Euro)
87. Runde:..... (41400 Euro)
88. Runde:..... (41400 Euro)
89. Runde:...160 .. (41560 Euro)
90. Runde:.160 .... (41720 Euro)
91. Runde:..... (41720 Euro)
92. Runde:..... (41720 Euro)
93. Runde:..... (41720 Euro)
94. Runde:..160 1k ... (42880 Euro)
95. Runde:.160 .... (43040 Euro)
96. Runde:....160 . (43200 Euro)
97. Runde:...160 .. (43360 Euro)
98. Runde:..... (43360 Euro)
99. Runde:....160 . (43520 Euro)
100. Runde:.....960  (44480 Euro)
===============================
Laufzeit: 29194 ms
===============================
43 Runden mit Nieten
0 Runden mit Verlust
0 Runden mit Einsatz erspielt
56 Runden mit Gewinn
1 Runden mit hohem Gewinn
===============================
Geldeinsatz: 5858 Euro
Gewinne: 44480 Euro
Saldo: 38622 Euro
Rundenschnitt: 440 Euro
===============================
1x 10k 0.9901%
2x 1k 1.9802%
27x 960 26.73267%
41x 160 40.59406%