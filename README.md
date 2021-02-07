# MyRover_kmgururaj

* Author: Gururaj Kulkarni
* Date: 07-Feb-2021
* Please see ViewMe.mp4 video to see behaviour of application.

*******************		Flow		*******************
1. On Page Load

	1.1 application reads CSV file which contaions commands to control rover 
		CSV file path is hardcoded in application "~\Guru.Rover.WebApplication\CsvData\Moments.csv
	
	1.2 Save commands in session (For this application commands are saved and its states are maintained in session.)

2. Render a button in UI to start / restart rover	

3. On click of button 
	
	3.1 Draw table (For this application number of rows (= 30) and columns (= 40) are hardcoded in application "Guru.DataProcessor/ApplicationConstants.cs")
	
	3.2 Get initial rover positions and render rovers in table as per command

4. Periodically get next command and move rover in UI (Periodicity is hardcoded in application to 3 Sec "Guru.DataProcessor/ApplicationConstants.cs")

5. Repeat step 4 until no more commands to process

		In UI Text displayed in format {0} | {1}X{2}Y{3} | {4}
		Where 
			{0} = Rover Name derived from CSV line number
			{1} = Current Compass position
				N - North
				E - East
				S - South
				W - West
			{2} = X axis coordinate
			{3} = Y axis coordinate
			{4} = Action just took place


*******************		Projects Structure		*******************

1. Guru.DataProcessor - All functionality related to rover 
	* DataProcessing
		-> CsvDataReader.cs - Read and parse CSV file
	* PositionHandler - Motion Control (Rotate and Move controls)
		-> RoverMoverHandler.cs - Rover movement control
		-> RoverRotatorHandler.cs - Rover rotation control
	* ApplicationConstants - Number of rows / columns, for demonstration added as constants.

2. Guru.Rover.WebApplication - All functionality related to displaying rovers in UI.
	* HomeController 
		-> GetAllRoversInitialData - Get initial landing positions to display in UI.
		-> GetNextCommand - Get next command for rover.

	* AppSession/Handler - Session management
		-> InitSession - CSV file path
		
	* Home/Index.cshtml - View to display rover
		-> View contains a button, click on it to (Re-) Start rover.

	* Scripts/Site/RoverApplication.js
		-> JS to control rover movement

3. Unit Test Projects
	* Guru.DataProcessor.Test - UT's related to Guru.DataProcessor
	* Guru.Rover.WebApplication.Test - UT's related to Guru.Rover.WebApplication
