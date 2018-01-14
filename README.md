# UltraCryptoFolio
## Version: 0.7

A simple webpage for myself to play with CryptoCurrency values and API's.

# Roadmap:

## Features / short term:
- When entering transactions able to enter values in Satoshi or Bitcoin.
- When entering transactions able to click "current value" for receiving currency.
- When entering transactions able to click "max" for sending all your available currency.
- In overview get 24h change of specific coins.
- Use Html5 web storage instead of cookies.

## Ideas / long term:
- Get a logo and some actual design instead of ugly basic bootstrap.
- Coin trends in graphs page.
- Support fees.
- Support exchange rate.

## Done:
- [x] Temporary solution for making too many calls on Value Over Time chart.
- [x] Basic graphs page.
- [x] Example data button to show off the website easily.
- [x] Doughnut graph of relative holdings in portfolio homepage.
- [x] Able to delete transactions.
- [x] Calculate value of transactions when they are entered.
- [x] Basic import / export.
- [x] Basic overview.
- [x] Basic entering of transactions.

## Known bugs:
- [] Value of portfolio graph for longer portfolio's has some incorrect 0 values. 
	-> Reason: request limit of price data is 15/second, 300/minute and 8000/hour
	-> Solution: find way to limit data calls, for now limit history to couple of weeks
- [] Hamburger menu since 0.6b not working on some mobile browsers.