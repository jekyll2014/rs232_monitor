# rs232_monitor
Collect COM ports (up to 4) data into 1 readable file for analyzing

Software intended for support engineers who needs to collect several data flows into a timed database to analyse the process flow.
T-cable drawing attached is the way to collect 2-way data from any RS232 connection. Sometimes one needs to collect up to several channels simultaneously so the maximum COM-port number extended to 4.
Real world example: analyse behavior of PC-fiscal controller-printer system. One needs to monitor:
1) Commands PC -> Fiscal
2) Replies Fiscal -> PC
3) Commands Fiscal -> Printer
4) Replies Printer -> Fiscal
