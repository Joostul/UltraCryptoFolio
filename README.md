# UltraCryptoFolio
## Version: 0.7a

A simple webpage for myself to play with CryptoCurrency values and API's.

# Roadmap:

## Features / short term:
- Tick which cryptocurrencies you want to see represented in charts.
- When entering transactions able to enter values in Satoshi or Bitcoin.
- When entering transactions able to click "current value" for receiving currency.
- When entering transactions able to click "max" for sending all your available currency.
- In overview get 24h change of specific coins.
- Use Html5 web storage instead of cookies.

## Ideas / long term:
- Support import of exchange exports.
- Coin trends in graphs page.
- Support fees.
- Support exchange rate.

## Done:
Version 0.7b
- [x] Fixed bug where hambuger menu wouldn't work on some browsers.
- [x] Add legend to ValueOverTime chart.
- [x] Fix tabs on charts page.
- [x] Update charts from canvas.js to chart.js.

Version 0.7:
- [x] Temporary solution for making too many calls on Value Over Time chart.
- [x] Basic charts page.

Version 0.6:
- [x] Example data button to show off the website easily.
- [x] Doughnut graph of relative holdings in portfolio homepage.

Version 0.5:
- [x] Able to delete transactions.

Version 0.4:
- [x] Calculate value of transactions when they are entered.

Version 0.3:
- [x] Basic import / export.

Version 0.2:
- [x] Basic overview.

Version 0.1:
- [x] Basic entering of transactions.

## Known bugs:
- [] Value of portfolio graph for longer portfolio's has some incorrect 0 values. 
	-> Reason: request limit of price data is 15/second, 300/minute and 8000/hour
	-> Solution: find way to limit data calls, for now limit history to couple of weeks