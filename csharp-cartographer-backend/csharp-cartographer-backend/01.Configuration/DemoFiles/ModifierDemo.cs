using System.Runtime.InteropServices;

namespace csharp_cartographer_backend._01.Configuration.DemoFiles
{
    // ----------------------------------------------------------------------------------
    //                                Access Modifiers
    // ----------------------------------------------------------------------------------

    // top-level types: public / internal
    public class PublicDemo
    {
    }

    internal class InternalDemo
    {
    }

    // nested types can use public / internal / protected / private / combinations
    class AccessModifierDemo
    {
        public class PublicNested
        {
        }

        internal class InternalNested
        {
        }

        protected class ProtectedNested
        {
        }

        private class PrivateNested
        {
        }

        protected internal class ProtectedInternalNested
        {
        }

        private protected class PrivateProtectedNested
        {
        }
    }


    // ----------------------------------------------------------------------------------
    //                             Polymorphism  Modifiers
    // ----------------------------------------------------------------------------------

    // abstract / virtual
    public abstract class AbstractBase
    {
        public abstract void AbstractMember();

        public virtual void VirtualMember() { }
    }

    // override
    public class OverrideDemo : AbstractBase
    {
        public override void AbstractMember() { }

        public override void VirtualMember() { }
    }

    // sealed
    public sealed class SealedType
    {
    }

    // sealed + override (prevents further overrides)
    public class SealedOverrideDemo : AbstractBase
    {
        public override void AbstractMember()
        {
        }

        public sealed override void VirtualMember()
        {
        }
    }


    // ----------------------------------------------------------------------------------
    //                                Member Modifiers
    // ----------------------------------------------------------------------------------

    // static
    public static class StaticMemberDemo
    {
        public static int StaticField;

        public static void StaticMethod()
        {
        }
    }

    // const / readonly / volatile
    public class ConstReadonlyVolatileDemo
    {
        public const int ConstValue = 1;

        public readonly int ReadonlyField = 2;

        public volatile int VolatileField = 3;
    }

    // partial
    public class PartialDemo
    {
        public partial class PartialTypeDemo
        {
            public void PartA()
            {
            }
        }

        public partial class PartialTypeDemo
        {
            public void PartB()
            {
            }
        }
    }

    // async
    public class AsyncDemo
    {
        public async Task AsyncMethod()
        {
            await Task.Delay(1);
        }
    }

    // new (member hiding)
    public class MemberHidingDemo
    {
        public class BaseWithMethod
        {
            public void DoWork()
            {
            }
        }

        public class NewHidingDemo : BaseWithMethod
        {
            public new void DoWork()
            {
            }
        }
    }

    // extern
    public class ExternDemo
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentThread();
    }

    // unsafe (requires compiling with /unsafe)
#if UNSAFE
    public unsafe class UnsafeDemo
    {
        public int* Ptr;
    }
#endif

    // unsafe
    public unsafe class UnsafeTypeDemo
    {
        public int* Ptr;
    }

    public class UnsafeMemberDemo
    {
        unsafe public int* Ptr;
    }

    public class UnsafeMethodDemo
    {
        unsafe public void DoWork()
        {
            int* ptr;
        }
    }

    public class UnsafeBlockDemo
    {
        public void DoWork()
        {
            unsafe
            {
                int* ptr;
            }
        }
    }

    // readonly struct
    public readonly struct ReadonlyStructDemo
    {
        public int X { get; }

        public ReadonlyStructDemo(int x) => X = x;
    }

    // readonly record struct
    public readonly record struct ReadonlyRecordStructDemo
    {
        public int X { get; }

        public ReadonlyRecordStructDemo(int x) => X = x;
    }

    // ref struct
    public ref struct RefStructDemo
    {
        public Span<int> Buffer;

        public RefStructDemo(Span<int> buffer) => Buffer = buffer;
    }


    // ----------------------------------------------------------------------------------
    //                                Parameter Modifiers
    // ----------------------------------------------------------------------------------

    public static class ParameterModifierDemo
    {
        public static void RefParam(ref int x)
        {
            x++;
        }

        public static void OutParam(out int x)
        {
            x = 123;
        }

        public static void InParam(in int x)
        {
            /* x is read-only */
        }

        public static int ParamsParam(params int[] values)
        {
            return values.Length;
        }

        // scoped (C# 11+): valid on ref/in parameters
        public static void ScopedRefParam(scoped ref int x)
        {
            x += 10;
        }

        public static void ScopedInParam(scoped in int x)
        {
            /* x is read-only */
        }
    }

    // this (extension method parameter modifier)
    public static class ExtensionMethodDemo
    {
        public static int WordCount(this string s) =>
            string.IsNullOrWhiteSpace(s)
                ? 0
                : s.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
    }


    // ----------------------------------------------------------------------------------
    //                              Argument Modifiers
    // ----------------------------------------------------------------------------------

    public static class ArgumentModifierDemo
    {
        public static void DemoCalls()
        {
            int x = 1;

            ParameterModifierDemo.RefParam(ref x);           // ref argument
            ParameterModifierDemo.OutParam(out int y);       // out argument
            ParameterModifierDemo.InParam(in x);             // in argument

            _ = ParameterModifierDemo.ParamsParam(1, 2, 3);  // params argument list

            _ = "hello world".WordCount(); // extension method call (uses 'this' parameter modifier)

            // scoped ref argument (C# 11+)
            ParameterModifierDemo.ScopedRefParam(ref x);
            ParameterModifierDemo.ScopedInParam(in x);
        }
    }
}
