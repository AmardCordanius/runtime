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

namespace JIT.HardwareIntrinsics.X86._Fma_Vector256
{
    public static partial class Program
    {
        [Fact]
        public static void MultiplyAddNegatedDouble()
        {
            var test = new SimpleTernaryOpTest__MultiplyAddNegatedDouble();

            if (test.IsSupported)
            {
                // Validates basic functionality works, using Unsafe.Read
                test.RunBasicScenario_UnsafeRead();

                if (Avx.IsSupported)
                {
                    // Validates basic functionality works, using Load
                    test.RunBasicScenario_Load();

                    // Validates basic functionality works, using LoadAligned
                    test.RunBasicScenario_LoadAligned();
                }

                // Validates calling via reflection works, using Unsafe.Read
                test.RunReflectionScenario_UnsafeRead();

                if (Avx.IsSupported)
                {
                    // Validates calling via reflection works, using Load
                    test.RunReflectionScenario_Load();

                    // Validates calling via reflection works, using LoadAligned
                    test.RunReflectionScenario_LoadAligned();
                }

                // Validates passing a static member works
                test.RunClsVarScenario();

                if (Avx.IsSupported)
                {
                    // Validates passing a static member works, using pinning and Load
                    test.RunClsVarScenario_Load();
                }

                // Validates passing a local works, using Unsafe.Read
                test.RunLclVarScenario_UnsafeRead();

                if (Avx.IsSupported)
                {
                    // Validates passing a local works, using Load
                    test.RunLclVarScenario_Load();

                    // Validates passing a local works, using LoadAligned
                    test.RunLclVarScenario_LoadAligned();
                }

                // Validates passing the field of a local class works
                test.RunClassLclFldScenario();

                if (Avx.IsSupported)
                {
                    // Validates passing the field of a local class works, using pinning and Load
                    test.RunClassLclFldScenario_Load();
                }

                // Validates passing an instance member of a class works
                test.RunClassFldScenario();

                if (Avx.IsSupported)
                {
                    // Validates passing an instance member of a class works, using pinning and Load
                    test.RunClassFldScenario_Load();
                }

                // Validates passing the field of a local struct works
                test.RunStructLclFldScenario();

                if (Avx.IsSupported)
                {
                    // Validates passing the field of a local struct works, using pinning and Load
                    test.RunStructLclFldScenario_Load();
                }

                // Validates passing an instance member of a struct works
                test.RunStructFldScenario();

                if (Avx.IsSupported)
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

    public sealed unsafe class SimpleTernaryOpTest__MultiplyAddNegatedDouble
    {
        private struct DataTable
        {
            private byte[] inArray1;
            private byte[] inArray2;
            private byte[] inArray3;
            private byte[] outArray;

            private GCHandle inHandle1;
            private GCHandle inHandle2;
            private GCHandle inHandle3;
            private GCHandle outHandle;

            private ulong alignment;

            public DataTable(Double[] inArray1, Double[] inArray2, Double[] inArray3, Double[] outArray, int alignment)
            {
                int sizeOfinArray1 = inArray1.Length * Unsafe.SizeOf<Double>();
                int sizeOfinArray2 = inArray2.Length * Unsafe.SizeOf<Double>();
                int sizeOfinArray3 = inArray3.Length * Unsafe.SizeOf<Double>();
                int sizeOfoutArray = outArray.Length * Unsafe.SizeOf<Double>();
                if ((alignment != 32 && alignment != 16) || (alignment * 2) < sizeOfinArray1 || (alignment * 2) < sizeOfinArray2 || (alignment * 2) < sizeOfinArray3 || (alignment * 2) < sizeOfoutArray)
                {
                    throw new ArgumentException("Invalid value of alignment");
                }

                this.inArray1 = new byte[alignment * 2];
                this.inArray2 = new byte[alignment * 2];
                this.inArray3 = new byte[alignment * 2];
                this.outArray = new byte[alignment * 2];

                this.inHandle1 = GCHandle.Alloc(this.inArray1, GCHandleType.Pinned);
                this.inHandle2 = GCHandle.Alloc(this.inArray2, GCHandleType.Pinned);
                this.inHandle3 = GCHandle.Alloc(this.inArray3, GCHandleType.Pinned);
                this.outHandle = GCHandle.Alloc(this.outArray, GCHandleType.Pinned);

                this.alignment = (ulong)alignment;

                Unsafe.CopyBlockUnaligned(ref Unsafe.AsRef<byte>(inArray1Ptr), ref Unsafe.As<Double, byte>(ref inArray1[0]), (uint)sizeOfinArray1);
                Unsafe.CopyBlockUnaligned(ref Unsafe.AsRef<byte>(inArray2Ptr), ref Unsafe.As<Double, byte>(ref inArray2[0]), (uint)sizeOfinArray2);
                Unsafe.CopyBlockUnaligned(ref Unsafe.AsRef<byte>(inArray3Ptr), ref Unsafe.As<Double, byte>(ref inArray3[0]), (uint)sizeOfinArray3);
            }

            public void* inArray1Ptr => Align((byte*)(inHandle1.AddrOfPinnedObject().ToPointer()), alignment);
            public void* inArray2Ptr => Align((byte*)(inHandle2.AddrOfPinnedObject().ToPointer()), alignment);
            public void* inArray3Ptr => Align((byte*)(inHandle3.AddrOfPinnedObject().ToPointer()), alignment);
            public void* outArrayPtr => Align((byte*)(outHandle.AddrOfPinnedObject().ToPointer()), alignment);

            public void Dispose()
            {
                inHandle1.Free();
                inHandle2.Free();
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
            public Vector256<Double> _fld1;
            public Vector256<Double> _fld2;
            public Vector256<Double> _fld3;

            public static TestStruct Create()
            {
                var testStruct = new TestStruct();

                for (var i = 0; i < Op1ElementCount; i++) { _data1[i] = TestLibrary.Generator.GetDouble(); }
                Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector256<Double>, byte>(ref testStruct._fld1), ref Unsafe.As<Double, byte>(ref _data1[0]), (uint)Unsafe.SizeOf<Vector256<Double>>());
                for (var i = 0; i < Op2ElementCount; i++) { _data2[i] = TestLibrary.Generator.GetDouble(); }
                Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector256<Double>, byte>(ref testStruct._fld2), ref Unsafe.As<Double, byte>(ref _data2[0]), (uint)Unsafe.SizeOf<Vector256<Double>>());
                for (var i = 0; i < Op3ElementCount; i++) { _data3[i] = TestLibrary.Generator.GetDouble(); }
                Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector256<Double>, byte>(ref testStruct._fld3), ref Unsafe.As<Double, byte>(ref _data3[0]), (uint)Unsafe.SizeOf<Vector256<Double>>());

                return testStruct;
            }

            public void RunStructFldScenario(SimpleTernaryOpTest__MultiplyAddNegatedDouble testClass)
            {
                var result = Fma.MultiplyAddNegated(_fld1, _fld2, _fld3);

                Unsafe.Write(testClass._dataTable.outArrayPtr, result);
                testClass.ValidateResult(_fld1, _fld2, _fld3, testClass._dataTable.outArrayPtr);
            }

            public void RunStructFldScenario_Load(SimpleTernaryOpTest__MultiplyAddNegatedDouble testClass)
            {
                fixed (Vector256<Double>* pFld1 = &_fld1)
                fixed (Vector256<Double>* pFld2 = &_fld2)
                fixed (Vector256<Double>* pFld3 = &_fld3)
                {
                    var result = Fma.MultiplyAddNegated(
                        Avx.LoadVector256((Double*)(pFld1)),
                        Avx.LoadVector256((Double*)(pFld2)),
                        Avx.LoadVector256((Double*)(pFld3))
                    );

                    Unsafe.Write(testClass._dataTable.outArrayPtr, result);
                    testClass.ValidateResult(_fld1, _fld2, _fld3, testClass._dataTable.outArrayPtr);
                }
            }
        }

        private static readonly int LargestVectorSize = 32;

        private static readonly int Op1ElementCount = Unsafe.SizeOf<Vector256<Double>>() / sizeof(Double);
        private static readonly int Op2ElementCount = Unsafe.SizeOf<Vector256<Double>>() / sizeof(Double);
        private static readonly int Op3ElementCount = Unsafe.SizeOf<Vector256<Double>>() / sizeof(Double);
        private static readonly int RetElementCount = Unsafe.SizeOf<Vector256<Double>>() / sizeof(Double);

        private static Double[] _data1 = new Double[Op1ElementCount];
        private static Double[] _data2 = new Double[Op2ElementCount];
        private static Double[] _data3 = new Double[Op3ElementCount];

        private static Vector256<Double> _clsVar1;
        private static Vector256<Double> _clsVar2;
        private static Vector256<Double> _clsVar3;

        private Vector256<Double> _fld1;
        private Vector256<Double> _fld2;
        private Vector256<Double> _fld3;

        private DataTable _dataTable;

        static SimpleTernaryOpTest__MultiplyAddNegatedDouble()
        {
            for (var i = 0; i < Op1ElementCount; i++) { _data1[i] = TestLibrary.Generator.GetDouble(); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector256<Double>, byte>(ref _clsVar1), ref Unsafe.As<Double, byte>(ref _data1[0]), (uint)Unsafe.SizeOf<Vector256<Double>>());
            for (var i = 0; i < Op2ElementCount; i++) { _data2[i] = TestLibrary.Generator.GetDouble(); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector256<Double>, byte>(ref _clsVar2), ref Unsafe.As<Double, byte>(ref _data2[0]), (uint)Unsafe.SizeOf<Vector256<Double>>());
            for (var i = 0; i < Op3ElementCount; i++) { _data3[i] = TestLibrary.Generator.GetDouble(); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector256<Double>, byte>(ref _clsVar3), ref Unsafe.As<Double, byte>(ref _data3[0]), (uint)Unsafe.SizeOf<Vector256<Double>>());
        }

        public SimpleTernaryOpTest__MultiplyAddNegatedDouble()
        {
            Succeeded = true;

            for (var i = 0; i < Op1ElementCount; i++) { _data1[i] = TestLibrary.Generator.GetDouble(); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector256<Double>, byte>(ref _fld1), ref Unsafe.As<Double, byte>(ref _data1[0]), (uint)Unsafe.SizeOf<Vector256<Double>>());
            for (var i = 0; i < Op2ElementCount; i++) { _data2[i] = TestLibrary.Generator.GetDouble(); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector256<Double>, byte>(ref _fld2), ref Unsafe.As<Double, byte>(ref _data2[0]), (uint)Unsafe.SizeOf<Vector256<Double>>());
            for (var i = 0; i < Op3ElementCount; i++) { _data3[i] = TestLibrary.Generator.GetDouble(); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector256<Double>, byte>(ref _fld3), ref Unsafe.As<Double, byte>(ref _data3[0]), (uint)Unsafe.SizeOf<Vector256<Double>>());

            for (var i = 0; i < Op1ElementCount; i++) { _data1[i] = TestLibrary.Generator.GetDouble(); }
            for (var i = 0; i < Op2ElementCount; i++) { _data2[i] = TestLibrary.Generator.GetDouble(); }
            for (var i = 0; i < Op3ElementCount; i++) { _data3[i] = TestLibrary.Generator.GetDouble(); }
            _dataTable = new DataTable(_data1, _data2, _data3, new Double[RetElementCount], LargestVectorSize);
        }

        public bool IsSupported => Fma.IsSupported;

        public bool Succeeded { get; set; }

        public void RunBasicScenario_UnsafeRead()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunBasicScenario_UnsafeRead));

            var result = Fma.MultiplyAddNegated(
                Unsafe.Read<Vector256<Double>>(_dataTable.inArray1Ptr),
                Unsafe.Read<Vector256<Double>>(_dataTable.inArray2Ptr),
                Unsafe.Read<Vector256<Double>>(_dataTable.inArray3Ptr)
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.inArray3Ptr, _dataTable.outArrayPtr);
        }

        public void RunBasicScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunBasicScenario_Load));

            var result = Fma.MultiplyAddNegated(
                Avx.LoadVector256((Double*)(_dataTable.inArray1Ptr)),
                Avx.LoadVector256((Double*)(_dataTable.inArray2Ptr)),
                Avx.LoadVector256((Double*)(_dataTable.inArray3Ptr))
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.inArray3Ptr, _dataTable.outArrayPtr);
        }

        public void RunBasicScenario_LoadAligned()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunBasicScenario_LoadAligned));

            var result = Fma.MultiplyAddNegated(
                Avx.LoadAlignedVector256((Double*)(_dataTable.inArray1Ptr)),
                Avx.LoadAlignedVector256((Double*)(_dataTable.inArray2Ptr)),
                Avx.LoadAlignedVector256((Double*)(_dataTable.inArray3Ptr))
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.inArray3Ptr, _dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_UnsafeRead()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunReflectionScenario_UnsafeRead));

            var result = typeof(Fma).GetMethod(nameof(Fma.MultiplyAddNegated), new Type[] { typeof(Vector256<Double>), typeof(Vector256<Double>), typeof(Vector256<Double>) })
                                     .Invoke(null, new object[] {
                                        Unsafe.Read<Vector256<Double>>(_dataTable.inArray1Ptr),
                                        Unsafe.Read<Vector256<Double>>(_dataTable.inArray2Ptr),
                                        Unsafe.Read<Vector256<Double>>(_dataTable.inArray3Ptr)
                                     });

            Unsafe.Write(_dataTable.outArrayPtr, (Vector256<Double>)(result));
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.inArray3Ptr, _dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunReflectionScenario_Load));

            var result = typeof(Fma).GetMethod(nameof(Fma.MultiplyAddNegated), new Type[] { typeof(Vector256<Double>), typeof(Vector256<Double>), typeof(Vector256<Double>) })
                                     .Invoke(null, new object[] {
                                        Avx.LoadVector256((Double*)(_dataTable.inArray1Ptr)),
                                        Avx.LoadVector256((Double*)(_dataTable.inArray2Ptr)),
                                        Avx.LoadVector256((Double*)(_dataTable.inArray3Ptr))
                                     });

            Unsafe.Write(_dataTable.outArrayPtr, (Vector256<Double>)(result));
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.inArray3Ptr, _dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_LoadAligned()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunReflectionScenario_LoadAligned));

            var result = typeof(Fma).GetMethod(nameof(Fma.MultiplyAddNegated), new Type[] { typeof(Vector256<Double>), typeof(Vector256<Double>), typeof(Vector256<Double>) })
                                     .Invoke(null, new object[] {
                                        Avx.LoadAlignedVector256((Double*)(_dataTable.inArray1Ptr)),
                                        Avx.LoadAlignedVector256((Double*)(_dataTable.inArray2Ptr)),
                                        Avx.LoadAlignedVector256((Double*)(_dataTable.inArray3Ptr))
                                     });

            Unsafe.Write(_dataTable.outArrayPtr, (Vector256<Double>)(result));
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.inArray3Ptr, _dataTable.outArrayPtr);
        }

        public void RunClsVarScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClsVarScenario));

            var result = Fma.MultiplyAddNegated(
                _clsVar1,
                _clsVar2,
                _clsVar3
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_clsVar1, _clsVar2, _clsVar3, _dataTable.outArrayPtr);
        }

        public void RunClsVarScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClsVarScenario_Load));

            fixed (Vector256<Double>* pClsVar1 = &_clsVar1)
            fixed (Vector256<Double>* pClsVar2 = &_clsVar2)
            fixed (Vector256<Double>* pClsVar3 = &_clsVar3)
            {
                var result = Fma.MultiplyAddNegated(
                    Avx.LoadVector256((Double*)(pClsVar1)),
                    Avx.LoadVector256((Double*)(pClsVar2)),
                    Avx.LoadVector256((Double*)(pClsVar3))
                );

                Unsafe.Write(_dataTable.outArrayPtr, result);
                ValidateResult(_clsVar1, _clsVar2, _clsVar3, _dataTable.outArrayPtr);
            }
        }

        public void RunLclVarScenario_UnsafeRead()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunLclVarScenario_UnsafeRead));

            var op1 = Unsafe.Read<Vector256<Double>>(_dataTable.inArray1Ptr);
            var op2 = Unsafe.Read<Vector256<Double>>(_dataTable.inArray2Ptr);
            var op3 = Unsafe.Read<Vector256<Double>>(_dataTable.inArray3Ptr);
            var result = Fma.MultiplyAddNegated(op1, op2, op3);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(op1, op2, op3, _dataTable.outArrayPtr);
        }

        public void RunLclVarScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunLclVarScenario_Load));

            var op1 = Avx.LoadVector256((Double*)(_dataTable.inArray1Ptr));
            var op2 = Avx.LoadVector256((Double*)(_dataTable.inArray2Ptr));
            var op3 = Avx.LoadVector256((Double*)(_dataTable.inArray3Ptr));
            var result = Fma.MultiplyAddNegated(op1, op2, op3);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(op1, op2, op3, _dataTable.outArrayPtr);
        }

        public void RunLclVarScenario_LoadAligned()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunLclVarScenario_LoadAligned));

            var op1 = Avx.LoadAlignedVector256((Double*)(_dataTable.inArray1Ptr));
            var op2 = Avx.LoadAlignedVector256((Double*)(_dataTable.inArray2Ptr));
            var op3 = Avx.LoadAlignedVector256((Double*)(_dataTable.inArray3Ptr));
            var result = Fma.MultiplyAddNegated(op1, op2, op3);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(op1, op2, op3, _dataTable.outArrayPtr);
        }

        public void RunClassLclFldScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClassLclFldScenario));

            var test = new SimpleTernaryOpTest__MultiplyAddNegatedDouble();
            var result = Fma.MultiplyAddNegated(test._fld1, test._fld2, test._fld3);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(test._fld1, test._fld2, test._fld3, _dataTable.outArrayPtr);
        }

        public void RunClassLclFldScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClassLclFldScenario_Load));

            var test = new SimpleTernaryOpTest__MultiplyAddNegatedDouble();

            fixed (Vector256<Double>* pFld1 = &test._fld1)
            fixed (Vector256<Double>* pFld2 = &test._fld2)
            fixed (Vector256<Double>* pFld3 = &test._fld3)
            {
                var result = Fma.MultiplyAddNegated(
                    Avx.LoadVector256((Double*)(pFld1)),
                    Avx.LoadVector256((Double*)(pFld2)),
                    Avx.LoadVector256((Double*)(pFld3))
                );

                Unsafe.Write(_dataTable.outArrayPtr, result);
                ValidateResult(test._fld1, test._fld2, test._fld3, _dataTable.outArrayPtr);
            }
        }

        public void RunClassFldScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClassFldScenario));

            var result = Fma.MultiplyAddNegated(_fld1, _fld2, _fld3);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_fld1, _fld2, _fld3, _dataTable.outArrayPtr);
        }

        public void RunClassFldScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClassFldScenario_Load));

            fixed (Vector256<Double>* pFld1 = &_fld1)
            fixed (Vector256<Double>* pFld2 = &_fld2)
            fixed (Vector256<Double>* pFld3 = &_fld3)
            {
                var result = Fma.MultiplyAddNegated(
                    Avx.LoadVector256((Double*)(pFld1)),
                    Avx.LoadVector256((Double*)(pFld2)),
                    Avx.LoadVector256((Double*)(pFld3))
                );

                Unsafe.Write(_dataTable.outArrayPtr, result);
                ValidateResult(_fld1, _fld2, _fld3, _dataTable.outArrayPtr);
            }
        }

        public void RunStructLclFldScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunStructLclFldScenario));

            var test = TestStruct.Create();
            var result = Fma.MultiplyAddNegated(test._fld1, test._fld2, test._fld3);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(test._fld1, test._fld2, test._fld3, _dataTable.outArrayPtr);
        }

        public void RunStructLclFldScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunStructLclFldScenario_Load));

            var test = TestStruct.Create();
            var result = Fma.MultiplyAddNegated(
                Avx.LoadVector256((Double*)(&test._fld1)),
                Avx.LoadVector256((Double*)(&test._fld2)),
                Avx.LoadVector256((Double*)(&test._fld3))
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(test._fld1, test._fld2, test._fld3, _dataTable.outArrayPtr);
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

        private void ValidateResult(Vector256<Double> op1, Vector256<Double> op2, Vector256<Double> op3, void* result, [CallerMemberName] string method = "")
        {
            Double[] inArray1 = new Double[Op1ElementCount];
            Double[] inArray2 = new Double[Op2ElementCount];
            Double[] inArray3 = new Double[Op3ElementCount];
            Double[] outArray = new Double[RetElementCount];

            Unsafe.WriteUnaligned(ref Unsafe.As<Double, byte>(ref inArray1[0]), op1);
            Unsafe.WriteUnaligned(ref Unsafe.As<Double, byte>(ref inArray2[0]), op2);
            Unsafe.WriteUnaligned(ref Unsafe.As<Double, byte>(ref inArray3[0]), op3);
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Double, byte>(ref outArray[0]), ref Unsafe.AsRef<byte>(result), (uint)Unsafe.SizeOf<Vector256<Double>>());

            ValidateResult(inArray1, inArray2, inArray3, outArray, method);
        }

        private void ValidateResult(void* op1, void* op2, void* op3, void* result, [CallerMemberName] string method = "")
        {
            Double[] inArray1 = new Double[Op1ElementCount];
            Double[] inArray2 = new Double[Op2ElementCount];
            Double[] inArray3 = new Double[Op3ElementCount];
            Double[] outArray = new Double[RetElementCount];

            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Double, byte>(ref inArray1[0]), ref Unsafe.AsRef<byte>(op1), (uint)Unsafe.SizeOf<Vector256<Double>>());
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Double, byte>(ref inArray2[0]), ref Unsafe.AsRef<byte>(op2), (uint)Unsafe.SizeOf<Vector256<Double>>());
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Double, byte>(ref inArray3[0]), ref Unsafe.AsRef<byte>(op3), (uint)Unsafe.SizeOf<Vector256<Double>>());
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Double, byte>(ref outArray[0]), ref Unsafe.AsRef<byte>(result), (uint)Unsafe.SizeOf<Vector256<Double>>());

            ValidateResult(inArray1, inArray2, inArray3, outArray, method);
        }

        private void ValidateResult(Double[] firstOp, Double[] secondOp, Double[] thirdOp, Double[] result, [CallerMemberName] string method = "")
        {
            bool succeeded = true;

            if (BitConverter.DoubleToInt64Bits(Math.Round(double.FusedMultiplyAdd(-firstOp[0], secondOp[0], thirdOp[0]), 9)) != BitConverter.DoubleToInt64Bits(Math.Round(result[0], 9)))
            {
                succeeded = false;
            }
            else
            {
                for (var i = 1; i < RetElementCount; i++)
                {
                    if (BitConverter.DoubleToInt64Bits(Math.Round(double.FusedMultiplyAdd(-firstOp[i], secondOp[i], thirdOp[i]), 9)) != BitConverter.DoubleToInt64Bits(Math.Round(result[i], 9)))
                    {
                        succeeded = false;
                        break;
                    }
                }
            }

            if (!succeeded)
            {
                TestLibrary.TestFramework.LogInformation($"{nameof(Fma)}.{nameof(Fma.MultiplyAddNegated)}<Double>(Vector256<Double>, Vector256<Double>, Vector256<Double>): {method} failed:");
                TestLibrary.TestFramework.LogInformation($" firstOp: ({string.Join(", ", firstOp)})");
                TestLibrary.TestFramework.LogInformation($"secondOp: ({string.Join(", ", secondOp)})");
                TestLibrary.TestFramework.LogInformation($" thirdOp: ({string.Join(", ", thirdOp)})");
                TestLibrary.TestFramework.LogInformation($"  result: ({string.Join(", ", result)})");
                TestLibrary.TestFramework.LogInformation(string.Empty);

                Succeeded = false;
            }
        }
    }
}
