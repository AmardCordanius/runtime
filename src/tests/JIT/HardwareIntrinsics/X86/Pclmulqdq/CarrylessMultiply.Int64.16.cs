// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

/******************************************************************************
 * This file is auto-generated from a template file by the GenerateTests.csx  *
 * script in tests\src\JIT\HardwareIntrinsics\X86\Shared. In order to make    *
 * changes, please update the corresponding template and run according to the *
 * directions listed in the file.                                             *
 ******************************************************************************/

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using Xunit;

namespace JIT.HardwareIntrinsics.X86._Pclmulqdq
{
    public static partial class Program
    {
        [Fact]
        public static void CarrylessMultiplyInt6416()
        {
            var test = new PclmulqdqOpTest__CarrylessMultiplyInt6416();

            if (test.IsSupported)
            {
                // Validates basic functionality works, using Unsafe.Read
                test.RunBasicScenario_UnsafeRead();

                if (Pclmulqdq.IsSupported)
                {
                    // Validates basic functionality works, using Load
                    test.RunBasicScenario_Load();

                    // Validates basic functionality works, using LoadAligned
                    test.RunBasicScenario_LoadAligned();
                }

                // Validates calling via reflection works, using Unsafe.Read
                test.RunReflectionScenario_UnsafeRead();

                if (Pclmulqdq.IsSupported)
                {
                    // Validates calling via reflection works, using Load
                    test.RunReflectionScenario_Load();

                    // Validates calling via reflection works, using LoadAligned
                    test.RunReflectionScenario_LoadAligned();
                }

                // Validates passing a static member works
                test.RunClsVarScenario();

                // Validates passing a local works, using Unsafe.Read
                test.RunLclVarScenario_UnsafeRead();

                if (Pclmulqdq.IsSupported)
                {
                    // Validates passing a local works, using Load
                    test.RunLclVarScenario_Load();

                    // Validates passing a local works, using LoadAligned
                    test.RunLclVarScenario_LoadAligned();
                }

                // Validates passing the field of a local class works
                test.RunClassLclFldScenario();

                // Validates passing an instance member of a class works
                test.RunClassFldScenario();

                // Validates passing the field of a local struct works
                test.RunStructLclFldScenario();

                // Validates passing an instance member of a struct works
                test.RunStructFldScenario();
            }
            else
            {
                // Validates we throw on unsupported hardware
                test.RunUnsupportedScenario();
            }

            if (!test.Succeeded)
            {
                throw new Exception("One or more scenarios did not complete as expected.");
            }
        }
    }

    public sealed unsafe class PclmulqdqOpTest__CarrylessMultiplyInt6416
    {
        private struct TestStruct
        {
            public Vector128<Int64> _fld1;
            public Vector128<Int64> _fld2;

            public static TestStruct Create()
            {
                var testStruct = new TestStruct();

                Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Int64>, byte>(ref testStruct._fld1), ref Unsafe.As<Int64, byte>(ref _data1[0]), (uint)Unsafe.SizeOf<Vector128<Int64>>());

                Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Int64>, byte>(ref testStruct._fld2), ref Unsafe.As<Int64, byte>(ref _data2[0]), (uint)Unsafe.SizeOf<Vector128<Int64>>());
                return testStruct;
            }

            public void RunStructFldScenario(PclmulqdqOpTest__CarrylessMultiplyInt6416 testClass)
            {
                var result = Pclmulqdq.CarrylessMultiply(_fld1, _fld2, 16);

                Unsafe.Write(testClass._dataTable.outArrayPtr, result);
                testClass.ValidateResult(testClass._dataTable.outArrayPtr);
            }
        }

        private static readonly int LargestVectorSize = 16;

        private static readonly int RetElementCount = Unsafe.SizeOf<Vector128<Int64>>() / sizeof(Int64);

        private static Int64[] _data1 = new Int64[2] {-2, -20};
        private static Int64[] _data2 = new Int64[2] {25, 65535};
        private static Int64[] _expectedRet = new Int64[2] {43690, 21845};

        private static Vector128<Int64> _clsVar1;
        private static Vector128<Int64> _clsVar2;

        private Vector128<Int64> _fld1;
        private Vector128<Int64> _fld2;

        private SimpleBinaryOpTest__DataTable<Int64, Int64, Int64> _dataTable;

        static PclmulqdqOpTest__CarrylessMultiplyInt6416()
        {

            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Int64>, byte>(ref _clsVar1), ref Unsafe.As<Int64, byte>(ref _data1[0]), (uint)Unsafe.SizeOf<Vector128<Int64>>());
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Int64>, byte>(ref _clsVar2), ref Unsafe.As<Int64, byte>(ref _data2[0]), (uint)Unsafe.SizeOf<Vector128<Int64>>());
        }

        public PclmulqdqOpTest__CarrylessMultiplyInt6416()
        {
            Succeeded = true;

            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Int64>, byte>(ref _fld1), ref Unsafe.As<Int64, byte>(ref _data1[0]), (uint)Unsafe.SizeOf<Vector128<Int64>>());

            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Int64>, byte>(ref _fld2), ref Unsafe.As<Int64, byte>(ref _data2[0]), (uint)Unsafe.SizeOf<Vector128<Int64>>());

            _dataTable = new SimpleBinaryOpTest__DataTable<Int64, Int64, Int64>(_data1, _data2, new Int64[RetElementCount], LargestVectorSize);
        }

        public bool IsSupported => Pclmulqdq.IsSupported;

        public bool Succeeded { get; set; }

        public void RunBasicScenario_UnsafeRead()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunBasicScenario_UnsafeRead));

            var result = Pclmulqdq.CarrylessMultiply(
                Unsafe.Read<Vector128<Int64>>(_dataTable.inArray1Ptr),
                Unsafe.Read<Vector128<Int64>>(_dataTable.inArray2Ptr),
                16
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.outArrayPtr);
        }

        public void RunBasicScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunBasicScenario_Load));

            var result = Pclmulqdq.CarrylessMultiply(
                Pclmulqdq.LoadVector128((Int64*)(_dataTable.inArray1Ptr)),
                Pclmulqdq.LoadVector128((Int64*)(_dataTable.inArray2Ptr)),
                16
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.outArrayPtr);
        }

        public void RunBasicScenario_LoadAligned()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunBasicScenario_LoadAligned));

            var result = Pclmulqdq.CarrylessMultiply(
                Pclmulqdq.LoadAlignedVector128((Int64*)(_dataTable.inArray1Ptr)),
                Pclmulqdq.LoadAlignedVector128((Int64*)(_dataTable.inArray2Ptr)),
                16
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_UnsafeRead()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunReflectionScenario_UnsafeRead));

            var result = typeof(Pclmulqdq).GetMethod(nameof(Pclmulqdq.CarrylessMultiply), new Type[] { typeof(Vector128<Int64>), typeof(Vector128<Int64>), typeof(byte) })
                                     .Invoke(null, new object[] {
                                        Unsafe.Read<Vector128<Int64>>(_dataTable.inArray1Ptr),
                                        Unsafe.Read<Vector128<Int64>>(_dataTable.inArray2Ptr),
                                        (byte)16
                                     });

            Unsafe.Write(_dataTable.outArrayPtr, (Vector128<Int64>)(result));
            ValidateResult(_dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunReflectionScenario_Load));

            var result = typeof(Pclmulqdq).GetMethod(nameof(Pclmulqdq.CarrylessMultiply), new Type[] { typeof(Vector128<Int64>), typeof(Vector128<Int64>), typeof(byte) })
                                     .Invoke(null, new object[] {
                                        Pclmulqdq.LoadVector128((Int64*)(_dataTable.inArray1Ptr)),
                                        Pclmulqdq.LoadVector128((Int64*)(_dataTable.inArray2Ptr)),
                                        (byte)16
                                     });

            Unsafe.Write(_dataTable.outArrayPtr, (Vector128<Int64>)(result));
            ValidateResult(_dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_LoadAligned()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunReflectionScenario_LoadAligned));

            var result = typeof(Pclmulqdq).GetMethod(nameof(Pclmulqdq.CarrylessMultiply), new Type[] { typeof(Vector128<Int64>), typeof(Vector128<Int64>), typeof(byte) })
                                     .Invoke(null, new object[] {
                                        Pclmulqdq.LoadAlignedVector128((Int64*)(_dataTable.inArray1Ptr)),
                                        Pclmulqdq.LoadAlignedVector128((Int64*)(_dataTable.inArray2Ptr)),
                                        (byte)16
                                     });

            Unsafe.Write(_dataTable.outArrayPtr, (Vector128<Int64>)(result));
            ValidateResult(_dataTable.outArrayPtr);
        }

        public void RunClsVarScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClsVarScenario));

            var result = Pclmulqdq.CarrylessMultiply(
                _clsVar1,
                _clsVar2,
                16
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.outArrayPtr);
        }

        public void RunLclVarScenario_UnsafeRead()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunLclVarScenario_UnsafeRead));

            var left = Unsafe.Read<Vector128<Int64>>(_dataTable.inArray1Ptr);
            var right = Unsafe.Read<Vector128<Int64>>(_dataTable.inArray2Ptr);
            var result = Pclmulqdq.CarrylessMultiply(left, right, 16);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.outArrayPtr);
        }

        public void RunLclVarScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunLclVarScenario_Load));

            var left = Pclmulqdq.LoadVector128((Int64*)(_dataTable.inArray1Ptr));
            var right = Pclmulqdq.LoadVector128((Int64*)(_dataTable.inArray2Ptr));
            var result = Pclmulqdq.CarrylessMultiply(left, right, 16);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.outArrayPtr);
        }

        public void RunLclVarScenario_LoadAligned()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunLclVarScenario_LoadAligned));

            var left = Pclmulqdq.LoadAlignedVector128((Int64*)(_dataTable.inArray1Ptr));
            var right = Pclmulqdq.LoadAlignedVector128((Int64*)(_dataTable.inArray2Ptr));
            var result = Pclmulqdq.CarrylessMultiply(left, right, 16);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.outArrayPtr);
        }

        public void RunClassLclFldScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClassLclFldScenario));

            var test = new PclmulqdqOpTest__CarrylessMultiplyInt6416();
            var result = Pclmulqdq.CarrylessMultiply(test._fld1, test._fld2, 16);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.outArrayPtr);
        }

        public void RunClassFldScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClassFldScenario));

            var result = Pclmulqdq.CarrylessMultiply(_fld1, _fld2, 16);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.outArrayPtr);
        }

        public void RunStructLclFldScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunStructLclFldScenario));

            var test = TestStruct.Create();
            var result = Pclmulqdq.CarrylessMultiply(test._fld1, test._fld2, 16);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.outArrayPtr);
        }

        public void RunStructFldScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunStructFldScenario));

            var test = TestStruct.Create();
            test.RunStructFldScenario(this);
        }

        public void RunUnsupportedScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunUnsupportedScenario));

            bool succeeded = false;

            try
            {
                RunBasicScenario_UnsafeRead();
            }
            catch (PlatformNotSupportedException)
            {
                succeeded = true;
            }

            if (!succeeded)
            {
                Succeeded = false;
            }
        }

        private void ValidateResult(void* result, [CallerMemberName] string method = "")
        {

            Int64[] outArray = new Int64[RetElementCount];

            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Int64, byte>(ref outArray[0]), ref Unsafe.AsRef<byte>(result), (uint)Unsafe.SizeOf<Vector128<Int64>>());

            ValidateResult(outArray, method);
        }

        private void ValidateResult(Int64[] result, [CallerMemberName] string method = "")
        {
            bool succeeded = true;

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] != _expectedRet[i] )
                {
                    succeeded = false;
                } 
            }

            if (!succeeded)
            {
                TestLibrary.TestFramework.LogInformation($"{nameof(Pclmulqdq)}.{nameof(Pclmulqdq.CarrylessMultiply)}<Int64>(Vector128<Int64>, 16): {method} failed:");
                TestLibrary.TestFramework.LogInformation($"  expectedRet: ({string.Join(", ", _expectedRet)})");
                TestLibrary.TestFramework.LogInformation($"  result: ({string.Join(", ", result)})");
                TestLibrary.TestFramework.LogInformation(string.Empty);

                Succeeded = false;
            }
        }

    }
}
