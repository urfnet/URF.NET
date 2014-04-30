for /R %%i in (bin,obj) do (
	rd /s/q "%%i"
)
