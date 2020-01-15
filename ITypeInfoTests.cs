// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Drawing;
using Xunit;
using static Interop;
using static Interop.Ole32;
using static Interop.Oleaut32;

namespace System.Windows.Forms.Primitives.Tests.Interop.Oleaut32
{
    public class ITypeInfoTests
    {
        [WinFormsFact]
        public unsafe void ITypeInfo_AddressOfMember_Invoke_Success()
        {
            using var image = new Bitmap(16, 32);
            IPictureDisp picture = SubAxHost.GetIPictureDispFromPicture(image);
            IDispatch dispatch = (IDispatch)picture;
            ITypeInfo typeInfo;
            HRESULT hr = dispatch.GetTypeInfo(0, 0, out typeInfo);
            Assert.Equal(HRESULT.S_OK, hr);

            IntPtr pv = (IntPtr)int.MaxValue;
            hr = typeInfo.AddressOfMember((DispatchID)6, INVOKEKIND.FUNC, &pv);
            Assert.Equal(HRESULT.TYPE_E_BADMODULEKIND, hr);
            Assert.Equal(IntPtr.Zero, pv);
        }

        [WinFormsFact]
        public unsafe void ITypeInfo_CreateInstance_Invoke_Success()
        {
            using var image = new Bitmap(16, 32);
            IPictureDisp picture = SubAxHost.GetIPictureDispFromPicture(image);
            IDispatch dispatch = (IDispatch)picture;
            ITypeInfo typeInfo;
            HRESULT hr = dispatch.GetTypeInfo(0, 0, out typeInfo);
            Assert.Equal(HRESULT.S_OK, hr);

            Guid riid = typeof(IPictureDisp).GUID;
            IntPtr pvObj = (IntPtr)int.MaxValue;
            hr = typeInfo.CreateInstance(IntPtr.Zero, &riid, &pvObj);
            Assert.Equal(HRESULT.TYPE_E_BADMODULEKIND, hr);
            Assert.Equal(IntPtr.Zero, pvObj);
        }

        [WinFormsFact]
        public unsafe void ITypeInfo_GetContainingTypeLib_Invoke_Success()
        {
            using var image = new Bitmap(16, 32);
            IPictureDisp picture = SubAxHost.GetIPictureDispFromPicture(image);
            IDispatch dispatch = (IDispatch)picture;
            ITypeInfo typeInfo;
            HRESULT hr = dispatch.GetTypeInfo(0, 0, out typeInfo);
            Assert.Equal(HRESULT.S_OK, hr);

            IntPtr typeLib = (IntPtr)int.MaxValue;
            uint index = uint.MaxValue;
            hr = typeInfo.GetContainingTypeLib(&typeLib, &index);
            try
            {
                Assert.Equal(HRESULT.S_OK, hr);
                Assert.NotEqual(IntPtr.Zero, typeLib);
                Assert.NotEqual(0u, index);
            }
            finally
            {
                Runtime.InteropServices.Marshal.Release(typeLib);
            }
        }

        [WinFormsFact]
        public unsafe void ITypeInfo_GetDllEntry_Invoke_Success()
        {
            using var image = new Bitmap(16, 32);
            IPictureDisp picture = SubAxHost.GetIPictureDispFromPicture(image);
            IDispatch dispatch = (IDispatch)picture;
            ITypeInfo typeInfo;
            HRESULT hr = dispatch.GetTypeInfo(0, 0, out typeInfo);
            Assert.Equal(HRESULT.S_OK, hr);

            var dllName = new string[] { "DllName" };
            var name = new string[] { "Name" };
            ushort wOrdinal = ushort.MaxValue;
            hr = typeInfo.GetDllEntry((DispatchID)6, INVOKEKIND.FUNC, dllName, name, &wOrdinal);
            Assert.Equal(HRESULT.TYPE_E_BADMODULEKIND, hr);
            Assert.Equal(new string[] { null }, dllName);
            Assert.Equal(new string[] { null }, name);
            Assert.Equal(0u, wOrdinal);
        }

        [WinFormsFact]
        public unsafe void ITypeInfo_GetDocumentation_Invoke_Success()
        {
            using var image = new Bitmap(16, 32);
            IPictureDisp picture = SubAxHost.GetIPictureDispFromPicture(image);
            IDispatch dispatch = (IDispatch)picture;
            ITypeInfo typeInfo;
            HRESULT hr = dispatch.GetTypeInfo(0, 0, out typeInfo);
            Assert.Equal(HRESULT.S_OK, hr);

            string name = "Name";
            string docString = "DocString";
            uint dwHelpContext = uint.MaxValue;
            var helpFile = new string[] { "HelpFile" };
            hr = typeInfo.GetDocumentation((DispatchID)4, ref name, ref docString, &dwHelpContext, helpFile);
            Assert.Equal(HRESULT.S_OK, hr);
            Assert.Equal("Width", name);
            Assert.Null(docString);
            Assert.Equal(0u, dwHelpContext);
            Assert.Equal(new string[] { null }, helpFile);
        }

        [WinFormsFact]
        public unsafe void ITypeInfo_GetFuncDesc_Invoke_Success()
        {
            using var image = new Bitmap(16, 32);
            IPictureDisp picture = SubAxHost.GetIPictureDispFromPicture(image);
            IDispatch dispatch = (IDispatch)picture;
            ITypeInfo typeInfo;
            HRESULT hr = dispatch.GetTypeInfo(0, 0, out typeInfo);
            Assert.Equal(HRESULT.S_OK, hr);

            FUNCDESC* pFuncDesc = null;
            try
            {
                hr = typeInfo.GetFuncDesc(0, &pFuncDesc);
                Assert.Equal(HRESULT.S_OK, hr);
                Assert.Equal((DispatchID)6, pFuncDesc->memid);
                Assert.Equal(IntPtr.Zero, pFuncDesc->lprgscode);
                Assert.NotEqual(IntPtr.Zero, (IntPtr)pFuncDesc->lprgelemdescParam);
                Assert.Equal(FUNCKIND.DISPATCH, pFuncDesc->funckind);
                Assert.Equal(INVOKEKIND.FUNC, pFuncDesc->invkind);
                Assert.Equal(CALLCONV.STDCALL, pFuncDesc->callconv);
                Assert.Equal(10, pFuncDesc->cParams);
                Assert.Equal(0, pFuncDesc->cParamsOpt);
                Assert.Equal(0, pFuncDesc->oVft);
                Assert.Equal(0, pFuncDesc->cScodes);
                Assert.Equal(VARENUM.VOID, pFuncDesc->elemdescFunc.tdesc.vt);
                Assert.Equal(IntPtr.Zero, pFuncDesc->elemdescFunc.tdesc.union.lpadesc);
                Assert.Equal(IntPtr.Zero, pFuncDesc->elemdescFunc.paramdesc.pparamdescex);
                Assert.Equal(IntPtr.Zero, pFuncDesc->elemdescFunc.paramdesc.pparamdescex);
            }
            finally
            {
                typeInfo.ReleaseFuncDesc(pFuncDesc);
            }
        }

        [WinFormsFact]
        public unsafe void ITypeInfo_GetIDsOfNames_Invoke_Success()
        {
            using var image = new Bitmap(16, 32);
            IPictureDisp picture = SubAxHost.GetIPictureDispFromPicture(image);
            IDispatch dispatch = (IDispatch)picture;
            ITypeInfo typeInfo;
            HRESULT hr = dispatch.GetTypeInfo(0, 0, out typeInfo);
            Assert.Equal(HRESULT.S_OK, hr);

            var rgszNames = new string[] { "Width", "Other" };
            var rgDispId = new DispatchID[rgszNames.Length];
            fixed (DispatchID* pRgDispId = rgDispId)
            {
                hr = typeInfo.GetIDsOfNames(rgszNames, (uint)rgszNames.Length, pRgDispId);
                Assert.Equal(HRESULT.S_OK, hr);
                Assert.Equal(new string[] { "Width", "Other" }, rgszNames);
                Assert.Equal(new DispatchID[] { (DispatchID)4, DispatchID.UNKNOWN }, rgDispId);
            }
        }

        [WinFormsFact]
        public unsafe void ITypeInfo_GetImplTypeFlags_Invoke_Success()
        {
            using var image = new Bitmap(16, 32);
            IPictureDisp picture = SubAxHost.GetIPictureDispFromPicture(image);
            IDispatch dispatch = (IDispatch)picture;
            ITypeInfo typeInfo;
            HRESULT hr = dispatch.GetTypeInfo(0, 0, out typeInfo);
            Assert.Equal(HRESULT.S_OK, hr);

            IMPLTYPEFLAG implTypeFlags = (IMPLTYPEFLAG)(-1);
            hr = typeInfo.GetImplTypeFlags(0, &implTypeFlags);
            Assert.Equal(HRESULT.S_OK, hr);
            Assert.NotEqual(IMPLTYPEFLAG.FDEFAULT, implTypeFlags);
        }

        [WinFormsFact]
        public unsafe void ITypeInfo_GetMops_Invoke_Success()
        {
            using var image = new Bitmap(16, 32);
            IPictureDisp picture = SubAxHost.GetIPictureDispFromPicture(image);
            IDispatch dispatch = (IDispatch)picture;
            ITypeInfo typeInfo;
            HRESULT hr = dispatch.GetTypeInfo(0, 0, out typeInfo);
            Assert.Equal(HRESULT.S_OK, hr);

            var mops = new string[] { "Mops" };
            hr = typeInfo.GetMops((DispatchID)4, mops);
            Assert.Equal(HRESULT.S_OK, hr);
            Assert.Equal(new string[] { null }, mops);
        }

        [WinFormsFact]
        public unsafe void ITypeInfo_GetNames_Invoke_Success()
        {
            using var image = new Bitmap(16, 32);
            IPictureDisp picture = SubAxHost.GetIPictureDispFromPicture(image);
            IDispatch dispatch = (IDispatch)picture;
            ITypeInfo typeInfo;
            HRESULT hr = dispatch.GetTypeInfo(0, 0, out typeInfo);
            Assert.Equal(HRESULT.S_OK, hr);

            var rgszNames = new string[2];
            uint cNames = 0;
            hr = typeInfo.GetNames((DispatchID)4, rgszNames, (uint)rgszNames.Length, &cNames);
            Assert.Equal(HRESULT.S_OK, hr);
            Assert.Equal(new string[] { "Width", null }, rgszNames);
            Assert.Equal(1u, cNames);
        }

        [WinFormsFact]
        public unsafe void ITypeInfo_GetRefTypeInfo_Invoke_Success()
        {
            using var image = new Bitmap(16, 32);
            IPictureDisp picture = SubAxHost.GetIPictureDispFromPicture(image);
            IDispatch dispatch = (IDispatch)picture;
            ITypeInfo typeInfo;
            HRESULT hr = dispatch.GetTypeInfo(0, 0, out typeInfo);
            Assert.Equal(HRESULT.S_OK, hr);

            uint refType = uint.MaxValue;
            hr = typeInfo.GetRefTypeOfImplType(0, &refType);
            Assert.Equal(HRESULT.S_OK, hr);
            Assert.NotEqual(0u, refType);

            ITypeInfo refTypeInfo;
            hr = typeInfo.GetRefTypeInfo(refType, out refTypeInfo);
            Assert.Equal(HRESULT.S_OK, hr);
            Assert.NotNull(refTypeInfo);
        }

        [WinFormsFact]
        public unsafe void ITypeInfo_GetRefTypeOfImplType_Invoke_Success()
        {
            using var image = new Bitmap(16, 32);
            IPictureDisp picture = SubAxHost.GetIPictureDispFromPicture(image);
            IDispatch dispatch = (IDispatch)picture;
            ITypeInfo typeInfo;
            HRESULT hr = dispatch.GetTypeInfo(0, 0, out typeInfo);
            Assert.Equal(HRESULT.S_OK, hr);

            uint refType = uint.MaxValue;
            hr = typeInfo.GetRefTypeOfImplType(0, &refType);
            Assert.Equal(HRESULT.S_OK, hr);
            Assert.NotEqual(0u, refType);
        }

        [WinFormsFact]
        public unsafe void ITypeInfo_GetTypeAttr_Invoke_Success()
        {
            using var image = new Bitmap(16, 32);
            IPictureDisp picture = SubAxHost.GetIPictureDispFromPicture(image);
            IDispatch dispatch = (IDispatch)picture;
            ITypeInfo typeInfo;
            HRESULT hr = dispatch.GetTypeInfo(0, 0, out typeInfo);
            Assert.Equal(HRESULT.S_OK, hr);

            TYPEATTR* pTypeAttr = null;
            try
            {
                hr = typeInfo.GetTypeAttr(&pTypeAttr);
                Assert.Equal(HRESULT.S_OK, hr);
                Assert.Equal(typeof(IPictureDisp).GUID, pTypeAttr->guid);
                Assert.Equal(0u, pTypeAttr->lcid);
                Assert.Equal(0u, pTypeAttr->dwReserved);
                Assert.Equal(DispatchID.UNKNOWN, pTypeAttr->memidConstructor);
                Assert.Equal(DispatchID.UNKNOWN, pTypeAttr->memidDestructor);
                Assert.Equal(IntPtr.Zero, pTypeAttr->lpstrSchema);
                Assert.Equal((uint)IntPtr.Size, pTypeAttr->cbSizeInstance);
                Assert.Equal(TYPEKIND.DISPATCH, pTypeAttr->typekind);
                Assert.Equal(1, pTypeAttr->cFuncs);
                Assert.Equal(5, pTypeAttr->cVars);
                Assert.Equal(1, pTypeAttr->cImplTypes);
                Assert.Equal(7 * IntPtr.Size, pTypeAttr->cbSizeVft);
                Assert.Equal((ushort)IntPtr.Size, pTypeAttr->cbAlignment);
                Assert.Equal(0x1000, pTypeAttr->wTypeFlags);
                Assert.Equal(0, pTypeAttr->wMajorVerNum);
                Assert.Equal(0, pTypeAttr->wMinorVerNum);
                Assert.Equal(VARENUM.EMPTY, pTypeAttr->tdescAlias.vt);
                Assert.Equal(IntPtr.Zero, pTypeAttr->idldescType.dwReserved);
                Assert.Equal(IDLFLAG.NONE, pTypeAttr->idldescType.wIDLFlags);
            }
            finally
            {
                typeInfo.ReleaseTypeAttr(pTypeAttr);
            }
        }

        [WinFormsFact]
        public unsafe void ITypeInfo_GetTypeComp_Invoke_Success()
        {
            using var image = new Bitmap(16, 32);
            IPictureDisp picture = SubAxHost.GetIPictureDispFromPicture(image);
            IDispatch dispatch = (IDispatch)picture;
            ITypeInfo typeInfo;
            HRESULT hr = dispatch.GetTypeInfo(0, 0, out typeInfo);
            Assert.Equal(HRESULT.S_OK, hr);

            IntPtr typeComp = IntPtr.Zero;
            hr = typeInfo.GetTypeComp(&typeComp);
            try
            {
                Assert.Equal(HRESULT.S_OK, hr);
                Assert.NotEqual(IntPtr.Zero, typeComp);
            }
            finally
            {
                Runtime.InteropServices.Marshal.Release(typeComp);
            }
        }

        [WinFormsFact]
        public unsafe void ITypeInfo_GetVarDesc_Invoke_Success()
        {
            using var image = new Bitmap(16, 32);
            IPictureDisp picture = SubAxHost.GetIPictureDispFromPicture(image);
            IDispatch dispatch = (IDispatch)picture;
            ITypeInfo typeInfo;
            HRESULT hr = dispatch.GetTypeInfo(0, 0, out typeInfo);
            Assert.Equal(HRESULT.S_OK, hr);

            VARDESC* pVarDesc = null;
            try
            {
                hr = typeInfo.GetVarDesc(3, &pVarDesc);
                Assert.Equal(HRESULT.S_OK, hr);
                Assert.Equal((DispatchID)4, pVarDesc->memid);
                Assert.Equal(IntPtr.Zero, pVarDesc->lpstrSchema);
                Assert.Equal(IntPtr.Zero, pVarDesc->unionMember);
                Assert.Equal(VARENUM.USERDEFINED, pVarDesc->elemdescVar.tdesc.vt);
                Assert.NotEqual(IntPtr.Zero, pVarDesc->elemdescVar.tdesc.union.lpadesc);
                Assert.Equal(IntPtr.Zero, pVarDesc->elemdescVar.paramdesc.pparamdescex);
                Assert.Equal(PARAMFLAG.NONE, pVarDesc->elemdescVar.paramdesc.wParamFlags);
                Assert.Equal(VARFLAGS.FREADONLY, pVarDesc->wVarFlags);
                Assert.Equal(VARKIND.DISPATCH, pVarDesc->varkind);
            }
            finally
            {
                typeInfo.ReleaseVarDesc(pVarDesc);
            }
        }

        [WinFormsFact]
        public unsafe void ITypeInfo_Invoke_Invoke_Success()
        {
            using var image = new Bitmap(16, 32);
            IPictureDisp picture = SubAxHost.GetIPictureDispFromPicture(image);
            IDispatch dispatch = (IDispatch)picture;
            ITypeInfo typeInfo;
            HRESULT hr = dispatch.GetTypeInfo(0, 0, out typeInfo);
            Assert.Equal(HRESULT.S_OK, hr);

            var dispParams = new DISPPARAMS();
            var varResult = new object[1];
            var excepInfo = new EXCEPINFO();
            uint argErr = 0;
            hr = typeInfo.Invoke(
                picture,
                (DispatchID)4,
                DISPATCH.PROPERTYGET,
                &dispParams,
                varResult,
                &excepInfo,
                &argErr
            );
            Assert.Equal(HRESULT.DISP_E_MEMBERNOTFOUND, hr);
            Assert.Null(varResult[0]);
            Assert.Equal(0u, argErr);
        }

        private class SubAxHost : AxHost
        {
            private SubAxHost(string clsidString) : base(clsidString)
            {
            }

            public new static IPictureDisp GetIPictureDispFromPicture(Image image) => (IPictureDisp)AxHost.GetIPictureDispFromPicture(image);
        }
    }
}
