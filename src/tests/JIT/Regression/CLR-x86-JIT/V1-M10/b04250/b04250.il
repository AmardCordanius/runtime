// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

.assembly extern legacy library mscorlib {}
.assembly extern System.Console
{
  .publickeytoken = (B0 3F 5F 7F 11 D5 0A 3A )
  .ver 4:0:0:0
}
.assembly b04250
{
}
.class _simple {

.method static int32 main() {
.entrypoint
.maxstack	100

	ldc.i4 1
	ldc.i4 -1
	neg
	ceq
	ldc.i4 0x1
	sub
	
	ldc.i4 -1
	ldc.i4 1
	neg
	ceq
	ldc.i4 -1
	add

	or

ldc.i4 100
add
	ret
}
}
