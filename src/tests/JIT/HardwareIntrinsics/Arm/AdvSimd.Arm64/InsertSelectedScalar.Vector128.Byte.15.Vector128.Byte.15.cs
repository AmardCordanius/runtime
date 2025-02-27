// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

/******************************************************************************
 * This file is auto-generated from a template file by the GenerateTests.csx  *
 * script in tests\src\JIT\HardwareIntrinsics.Arm\Shared. In order to make    *
 * changes, please update the corresponding template and run according to the *
 * directions listed in the file.                                             *
 ******************************************************************************/

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using Xunit;

namespace JIT.HardwareIntrinsics.Arm._AdvSimd.Arm64
{
    public static partial class Program
    {
        [Fact]
        public static void InsertSelectedScalar_Vector128_Byte_15_Vector128_Byte_15()
        {
            var test = new InsertSelectedScalarTest__InsertSelectedScalar_Vector128_Byte_15_Vector128_Byte_15();

            if (test.IsSupported)
            {
                // Validates basic functionality works, using Unsafe.Read
                test.RunBasicScenario_UnsafeRead();

                if (AdvSimd.IsSupported)
                {
                    // Validates basic functionality works, using Load
                    test.RunBasicScenario_Load();
                }

                // Validates calling via reflection works, using Unsafe.Read
                test.RunReflectionScenario_UnsafeRead();

                if (AdvSimd.IsSupported)
                {
                    // Validates calling via reflection works, using Load
                    test.RunReflectionScenario_Load();
                }

                // Validates passing a static member works
                test.RunClsVarScenario();

                if (AdvSimd.IsSupported)
                {
                    // Validates passing a static member works, using pinning and Load
                    test.RunClsVarScenario_Load();
                }

                // Validates passing a local works, using Unsafe.Read
                test.RunLclVarScenario_UnsafeRead();

                if (AdvSimd.IsSupported)
                {
                    // Validates passing a local works, using Load
                    test.RunLclVarScenario_Load();
                }

                // Validates passing the field of a local class works
                test.RunClassLclFldScenario();

                if (AdvSimd.IsSupported)
                {
                    // Validates passing the field of a local class works, using pinning and Load
                    test.RunClassLclFldScenario_Load();
                }

                // Validates passing an instance member of a class works
                test.RunClassFldScenario();

                if (AdvSimd.IsSupported)
                {
                    // Validates passing an instance member of a class works, using pinning and Load
                    test.RunClassFldScenario_Load();
                }

                // Validates passing the field of a local struct works
                test.RunStructLclFldScenario();

                if (AdvSimd.IsSupported)
                {
                    // Validates passing the field of a local struct works, using pinning and Load
                    test.RunStructLclFldScenario_Load();
                }

                // Validates passing an instance member of a struct works
                test.RunStructFldScenario();

                if (AdvSimd.IsSupported)
                {
                    // Validates passing an instance member of a struct works, using pinning and Load
                    test.RunStructFldScenario_Load();
                }
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

    public sealed unsafe class InsertSelectedScalarTest__InsertSelectedScalar_Vector128_Byte_15_Vector128_Byte_15
    {
        private struct DataTable
        {
            private byte[] inArray1;
            private byte[] inArray3;
            private byte[] outArray;

            private GCHandle inHandle1;
            private GCHandle inHandle3;
            private GCHandle outHandle;

            private ulong alignment;

            public DataTable(Byte[] inArray1, Byte[] inArray3, Byte[] outArray, int alignment)
            {
                int sizeOfinArray1 = inArray1.Length * Unsafe.SizeOf<Byte>();
                int sizeOfinArray3 = inArray3.Length * Unsafe.SizeOf<Byte>();
                int sizeOfoutArray = outArray.Length * Unsafe.SizeOf<Byte>();
                if ((alignment != 16 && alignment != 8) || (alignment * 2) < sizeOfinArray1 || (alignment * 2) < sizeOfinArray3 || (alignment * 2) < sizeOfoutArray)
                {
                    throw new ArgumentException("Invalid value of alignment");
                }

                this.inArray1 = new byte[alignment * 2];
                this.inArray3 = new byte[alignment * 2];
                this.outArray = new byte[alignment * 2];

                this.inHandle1 = GCHandle.Alloc(this.inArray1, GCHandleType.Pinned);
                this.inHandle3 = GCHandle.Alloc(this.inArray3, GCHandleType.Pinned);
                this.outHandle = GCHandle.Alloc(this.outArray, GCHandleType.Pinned);

                this.alignment = (ulong)alignment;

                Unsafe.CopyBlockUnaligned(ref Unsafe.AsRef<byte>(inArray1Ptr), ref Unsafe.As<Byte, byte>(ref inArray1[0]), (uint)sizeOfinArray1);
                Unsafe.CopyBlockUnaligned(ref Unsafe.AsRef<byte>(inArray3Ptr), ref Unsafe.As<Byte, byte>(ref inArray3[0]), (uint)sizeOfinArray3);
            }

            public void* inArray1Ptr => Align((byte*)(inHandle1.AddrOfPinnedObject().ToPointer()), alignment);
            public void* inArray3Ptr => Align((byte*)(inHandle3.AddrOfPinnedObject().ToPointer()), alignment);
            public void* outArrayPtr => Align((byte*)(outHandle.AddrOfPinnedObject().ToPointer()), alignment);

            public void Dispose()
            {
                inHandle1.Free();
                inHandle3.Free();
                outHandle.Free();
            }

            private static unsafe void* Align(byte* buffer, ulong expectedAlignment)
            {
                return (void*)(((ulong)buffer + expectedAlignment - 1) & ~(expectedAlignment - 1));
            }
        }

        private struct TestStruct
        {
            public Vector128<Byte> _fld1;
            public Vector128<Byte> _fld3;

            public static TestStruct Create()
            {
                var testStruct = new TestStruct();

                for (var i = 0; i < Op1ElementCount; i++) { _data1[i] = TestLibrary.Generator.GetByte(); }
                Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Byte>, byte>(ref testStruct._fld1), ref Unsafe.As<Byte, byte>(ref _data1[0]), (uint)Unsafe.SizeOf<Vector128<Byte>>());
                for (var i = 0; i < Op3ElementCount; i++) { _data3[i] = TestLibrary.Generator.GetByte(); }
                Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Byte>, byte>(ref testStruct._fld3), ref Unsafe.As<Byte, byte>(ref _data3[0]), (uint)Unsafe.SizeOf<Vector128<Byte>>());

                return testStruct;
            }

            public void RunStructFldScenario(InsertSelectedScalarTest__InsertSelectedScalar_Vector128_Byte_15_Vector128_Byte_15 testClass)
            {
                var result = AdvSimd.Arm64.InsertSelectedScalar(_fld1, 15, _fld3, 15);

                Unsafe.Write(testClass._dataTable.outArrayPtr, result);
                testClass.ValidateResult(_fld1, _fld3, testClass._dataTable.outArrayPtr);
            }

            public void RunStructFldScenario_Load(InsertSelectedScalarTest__InsertSelectedScalar_Vector128_Byte_15_Vector128_Byte_15 testClass)
            {
                fixed (Vector128<Byte>* pFld1 = &_fld1)
                fixed (Vector128<Byte>* pFld2 = &_fld3)
                {
                    var result = AdvSimd.Arm64.InsertSelectedScalar(
                        AdvSimd.LoadVector128((Byte*)pFld1),
                        15,
                        AdvSimd.LoadVector128((Byte*)pFld2),
                        15
                    );

                    Unsafe.Write(testClass._dataTable.outArrayPtr, result);
                    testClass.ValidateResult(_fld1, _fld3, testClass._dataTable.outArrayPtr);
                }
            }
        }

        private static readonly int LargestVectorSize = 16;

        private static readonly int Op1ElementCount = Unsafe.SizeOf<Vector128<Byte>>() / sizeof(Byte);
        private static readonly int Op3ElementCount = Unsafe.SizeOf<Vector128<Byte>>() / sizeof(Byte);
        private static readonly int RetElementCount = Unsafe.SizeOf<Vector128<Byte>>() / sizeof(Byte);
        private static readonly byte ElementIndex1 = 15;
        private static readonly byte ElementIndex2 = 15;

        private static Byte[] _data1 = new Byte[Op1ElementCount];
        private static Byte[] _data3 = new Byte[Op3ElementCount];

        private static Vector128<Byte> _clsVar1;
        private static Vector128<Byte> _clsVar3;

        private Vector128<Byte> _fld1;
        private Vector128<Byte> _fld3;

        private DataTable _dataTable;

        static InsertSelectedScalarTest__InsertSelectedScalar_Vector128_Byte_15_Vector128_Byte_15()
        {
            for (var i = 0; i < Op1ElementCount; i++) { _data1[i] = TestLibrary.Generator.GetByte(); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Byte>, byte>(ref _clsVar1), ref Unsafe.As<Byte, byte>(ref _data1[0]), (uint)Unsafe.SizeOf<Vector128<Byte>>());
            for (var i = 0; i < Op3ElementCount; i++) { _data3[i] = TestLibrary.Generator.GetByte(); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Byte>, byte>(ref _clsVar3), ref Unsafe.As<Byte, byte>(ref _data3[0]), (uint)Unsafe.SizeOf<Vector128<Byte>>());
        }

        public InsertSelectedScalarTest__InsertSelectedScalar_Vector128_Byte_15_Vector128_Byte_15()
        {
            Succeeded = true;

            for (var i = 0; i < Op1ElementCount; i++) { _data1[i] = TestLibrary.Generator.GetByte(); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Byte>, byte>(ref _fld1), ref Unsafe.As<Byte, byte>(ref _data1[0]), (uint)Unsafe.SizeOf<Vector128<Byte>>());
            for (var i = 0; i < Op3ElementCount; i++) { _data3[i] = TestLibrary.Generator.GetByte(); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Byte>, byte>(ref _fld3), ref Unsafe.As<Byte, byte>(ref _data3[0]), (uint)Unsafe.SizeOf<Vector128<Byte>>());

            for (var i = 0; i < Op1ElementCount; i++) { _data1[i] = TestLibrary.Generator.GetByte(); }
            for (var i = 0; i < Op3ElementCount; i++) { _data3[i] = TestLibrary.Generator.GetByte(); }
            _dataTable = new DataTable(_data1, _data3, new Byte[RetElementCount], LargestVectorSize);
        }

        public bool IsSupported => AdvSimd.Arm64.IsSupported;

        public bool Succeeded { get; set; }

        public void RunBasicScenario_UnsafeRead()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunBasicScenario_UnsafeRead));

            var result = AdvSimd.Arm64.InsertSelectedScalar(
                Unsafe.Read<Vector128<Byte>>(_dataTable.inArray1Ptr),
                15,
                Unsafe.Read<Vector128<Byte>>(_dataTable.inArray3Ptr),
                15
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray3Ptr, _dataTable.outArrayPtr);
        }

        public void RunBasicScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunBasicScenario_Load));

            var result = AdvSimd.Arm64.InsertSelectedScalar(
                AdvSimd.LoadVector128((Byte*)(_dataTable.inArray1Ptr)),
                15,
                AdvSimd.LoadVector128((Byte*)(_dataTable.inArray3Ptr)),
                15
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray3Ptr, _dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_UnsafeRead()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunReflectionScenario_UnsafeRead));

            var result = typeof(AdvSimd.Arm64).GetMethod(nameof(AdvSimd.Arm64.InsertSelectedScalar), new Type[] { typeof(Vector128<Byte>), typeof(byte), typeof(Vector128<Byte>), typeof(byte) })
                                     .Invoke(null, new object[] {
                                        Unsafe.Read<Vector128<Byte>>(_dataTable.inArray1Ptr),
                                        ElementIndex1,
                                        Unsafe.Read<Vector128<Byte>>(_dataTable.inArray3Ptr),
                                        ElementIndex2
                                     });

            Unsafe.Write(_dataTable.outArrayPtr, (Vector128<Byte>)(result));
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray3Ptr, _dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunReflectionScenario_Load));

            var result = typeof(AdvSimd.Arm64).GetMethod(nameof(AdvSimd.Arm64.InsertSelectedScalar), new Type[] { typeof(Vector128<Byte>), typeof(byte), typeof(Vector128<Byte>), typeof(byte) })
                                     .Invoke(null, new object[] {
                                        AdvSimd.LoadVector128((Byte*)(_dataTable.inArray1Ptr)),
                                        ElementIndex1,
                                        AdvSimd.LoadVector128((Byte*)(_dataTable.inArray3Ptr)),
                                        ElementIndex2
                                     });

            Unsafe.Write(_dataTable.outArrayPtr, (Vector128<Byte>)(result));
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray3Ptr, _dataTable.outArrayPtr);
        }

        public void RunClsVarScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClsVarScenario));

            var result = AdvSimd.Arm64.InsertSelectedScalar(
                _clsVar1,
                15,
                _clsVar3,
                15
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_clsVar1, _clsVar3, _dataTable.outArrayPtr);
        }

        public void RunClsVarScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClsVarScenario_Load));

            fixed (Vector128<Byte>* pClsVar1 = &_clsVar1)
            fixed (Vector128<Byte>* pClsVar3 = &_clsVar3)
            {
                var result = AdvSimd.Arm64.InsertSelectedScalar(
                    AdvSimd.LoadVector128((Byte*)(pClsVar1)),
                    15,
                    AdvSimd.LoadVector128((Byte*)(pClsVar3)),
                    15
                );

                Unsafe.Write(_dataTable.outArrayPtr, result);
                ValidateResult(_clsVar1, _clsVar3, _dataTable.outArrayPtr);
            }
        }

        public void RunLclVarScenario_UnsafeRead()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunLclVarScenario_UnsafeRead));

            var op1 = Unsafe.Read<Vector128<Byte>>(_dataTable.inArray1Ptr);
            var op3 = Unsafe.Read<Vector128<Byte>>(_dataTable.inArray3Ptr);
            var result = AdvSimd.Arm64.InsertSelectedScalar(op1, 15, op3, 15);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(op1, op3, _dataTable.outArrayPtr);
        }

        public void RunLclVarScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunLclVarScenario_Load));

            var op1 = AdvSimd.LoadVector128((Byte*)(_dataTable.inArray1Ptr));
            var op3 = AdvSimd.LoadVector128((Byte*)(_dataTable.inArray3Ptr));
            var result = AdvSimd.Arm64.InsertSelectedScalar(op1, 15, op3, 15);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(op1, op3, _dataTable.outArrayPtr);
        }

        public void RunClassLclFldScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClassLclFldScenario));

            var test = new InsertSelectedScalarTest__InsertSelectedScalar_Vector128_Byte_15_Vector128_Byte_15();
            var result = AdvSimd.Arm64.InsertSelectedScalar(test._fld1, 15, test._fld3, 15);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(test._fld1, test._fld3, _dataTable.outArrayPtr);
        }

        public void RunClassLclFldScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClassLclFldScenario_Load));

            var test = new InsertSelectedScalarTest__InsertSelectedScalar_Vector128_Byte_15_Vector128_Byte_15();

            fixed (Vector128<Byte>* pFld1 = &test._fld1)
            fixed (Vector128<Byte>* pFld2 = &test._fld3)
            {
                var result = AdvSimd.Arm64.InsertSelectedScalar(
                    AdvSimd.LoadVector128((Byte*)pFld1),
                    15,
                    AdvSimd.LoadVector128((Byte*)pFld2),
                    15
                );

                Unsafe.Write(_dataTable.outArrayPtr, result);
                ValidateResult(test._fld1, test._fld3, _dataTable.outArrayPtr);
            }
        }

        public void RunClassFldScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClassFldScenario));

            var result = AdvSimd.Arm64.InsertSelectedScalar(_fld1, 15, _fld3, 15);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_fld1, _fld3, _dataTable.outArrayPtr);
        }

        public void RunClassFldScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClassFldScenario_Load));

            fixed (Vector128<Byte>* pFld1 = &_fld1)
            fixed (Vector128<Byte>* pFld2 = &_fld3)
            {
                var result = AdvSimd.Arm64.InsertSelectedScalar(
                    AdvSimd.LoadVector128((Byte*)pFld1),
                    15,
                    AdvSimd.LoadVector128((Byte*)pFld2),
                    15
                );

                Unsafe.Write(_dataTable.outArrayPtr, result);
                ValidateResult(_fld1, _fld3, _dataTable.outArrayPtr);
            }
        }

        public void RunStructLclFldScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunStructLclFldScenario));

            var test = TestStruct.Create();
            var result = AdvSimd.Arm64.InsertSelectedScalar(test._fld1, 15, test._fld3, 15);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(test._fld1, test._fld3, _dataTable.outArrayPtr);
        }

        public void RunStructLclFldScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunStructLclFldScenario_Load));

            var test = TestStruct.Create();
            var result = AdvSimd.Arm64.InsertSelectedScalar(
                AdvSimd.LoadVector128((Byte*)(&test._fld1)),
                15,
                AdvSimd.LoadVector128((Byte*)(&test._fld3)),
                15
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(test._fld1, test._fld3, _dataTable.outArrayPtr);
        }

        public void RunStructFldScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunStructFldScenario));

            var test = TestStruct.Create();
            test.RunStructFldScenario(this);
        }

        public void RunStructFldScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunStructFldScenario_Load));

            var test = TestStruct.Create();
            test.RunStructFldScenario_Load(this);
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

        private void ValidateResult(Vector128<Byte> op1, Vector128<Byte> op3, void* result, [CallerMemberName] string method = "")
        {
            Byte[] inArray1 = new Byte[Op1ElementCount];
            Byte[] inArray3 = new Byte[Op3ElementCount];
            Byte[] outArray = new Byte[RetElementCount];

            Unsafe.WriteUnaligned(ref Unsafe.As<Byte, byte>(ref inArray1[0]), op1);
            Unsafe.WriteUnaligned(ref Unsafe.As<Byte, byte>(ref inArray3[0]), op3);
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Byte, byte>(ref outArray[0]), ref Unsafe.AsRef<byte>(result), (uint)Unsafe.SizeOf<Vector128<Byte>>());

            ValidateResult(inArray1, inArray3, outArray, method);
        }

        private void ValidateResult(void* op1, void* op3, void* result, [CallerMemberName] string method = "")
        {
            Byte[] inArray1 = new Byte[Op1ElementCount];
            Byte[] inArray3 = new Byte[Op3ElementCount];
            Byte[] outArray = new Byte[RetElementCount];

            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Byte, byte>(ref inArray1[0]), ref Unsafe.AsRef<byte>(op1), (uint)Unsafe.SizeOf<Vector128<Byte>>());
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Byte, byte>(ref inArray3[0]), ref Unsafe.AsRef<byte>(op3), (uint)Unsafe.SizeOf<Vector128<Byte>>());
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Byte, byte>(ref outArray[0]), ref Unsafe.AsRef<byte>(result), (uint)Unsafe.SizeOf<Vector128<Byte>>());

            ValidateResult(inArray1, inArray3, outArray, method);
        }

        private void ValidateResult(Byte[] firstOp, Byte[] thirdOp, Byte[] result, [CallerMemberName] string method = "")
        {
            bool succeeded = true;

            for (var i = 0; i < RetElementCount; i++)
            {
                if (Helpers.Insert(firstOp, ElementIndex1, thirdOp[ElementIndex2], i) != result[i])
                {
                    succeeded = false;
                    break;
                }
            }

            if (!succeeded)
            {
                TestLibrary.TestFramework.LogInformation($"{nameof(AdvSimd.Arm64)}.{nameof(AdvSimd.Arm64.InsertSelectedScalar)}<Byte>(Vector128<Byte>, {15}, Vector128<Byte>, {15}): {method} failed:");
                TestLibrary.TestFramework.LogInformation($" firstOp: ({string.Join(", ", firstOp)})");
                TestLibrary.TestFramework.LogInformation($" thirdOp: ({string.Join(", ", thirdOp)})");
                TestLibrary.TestFramework.LogInformation($"  result: ({string.Join(", ", result)})");
                TestLibrary.TestFramework.LogInformation(string.Empty);

                Succeeded = false;
            }
        }
    }
}
