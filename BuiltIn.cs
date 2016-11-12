// BuiltIn -- the data structure for built-in functions

// Class BuiltIn is used for representing the value of built-in functions
// such as +.  Populate the initial environment with
// (name, new BuiltIn(name)) pairs.

// The object-oriented style for implementing built-in functions would be
// to include the C# methods for implementing a Scheme built-in in the
// BuiltIn object.  This could be done by writing one subclass of class
// BuiltIn for each built-in function and implementing the method apply
// appropriately.  This requires a large number of classes, though.
// Another alternative is to program BuiltIn.apply() in a functional
// style by writing a large if-then-else chain that tests the name of
// the function symbol.

using System;
using Parse;
using Tree;

namespace Tree
{
    public class BuiltIn : Node
    {
        private Node symbol;            // the Ident for the built-in function

        public BuiltIn(Node s)		{ symbol = s; }

        public Node getSymbol()		{ return symbol; }


        public override bool isProcedure()	{ return true; }

        public override void print(int n)
        {
            // there got to be a more efficient way to print n spaces
            for (int i = 0; i < n; i++)
                Console.Write(' ');
            Console.Write("#{Built-in Procedure ");
            if (symbol != null)
                symbol.print(-Math.Abs(n));
            Console.Write('}');
            if (n >= 0)
                Console.WriteLine();
        }

        // TODO: The method apply() should be defined in class Node
        // to report an error.  It should be overridden only in classes
        // BuiltIn and Closure.  Probably will have to include calls to eval
        public override Node apply(Node args)
        {
            if (args == null)
                return null;

            String Symbol = symbol.getName();
            Node arg1 = args.getCar(), arg2 = args.getCdr().getCar();
            switch (Symbol)
            {
                case "symbol?":
                    return BoolLit.getInstance(arg1.isSymbol());
                case "number?":
                    return BoolLit.getInstance(arg1.isNumber());
                case "b+":
                    return BinArith(((IntLit)arg1).getValue(), ((IntLit)arg2).getValue(), "+");
                case "b-":
                    return BinArith(((IntLit)arg1).getValue(), ((IntLit)arg2).getValue(), "-");
                case "b*":
                    return BinArith(((IntLit)arg1).getValue(), ((IntLit)arg2).getValue(), "*");
                case "b/":
                    return BinArith(((IntLit)arg1).getValue(), ((IntLit)arg2).getValue(), "/");
                case "b=":
                    return BinArith(((IntLit)arg1).getValue(), ((IntLit)arg2).getValue(), "=");
                case "b>":
                    return BinArith(((IntLit)arg1).getValue(), ((IntLit)arg2).getValue(), ">");
                case "b<":
                    return BinArith(((IntLit)arg1).getValue(), ((IntLit)arg2).getValue(), "<");
                case "car":  // ????????????????
                    if (arg1.isNull())
                        return arg1; //arg1==nil or null?
                    else
                        return arg1.getCar();

                case "cdr":  // ????????????????
                    if (arg1.isNull())
                        return arg1; //arg1==nil or null?
                    else
                        return arg1.getCdr();

                case "cons":
                    return new Cons(arg1, arg2);
                case "set-car!":
                    arg1.setCar(arg2);
                    return arg1;
                case "set-cdr!":
                    arg1.setCdr(arg2);
                    return arg1;
                case "null?":
                    return BoolLit.getInstance(arg1.isNull());
                case "pair?":
                    return BoolLit.getInstance(arg1.isPair());
                case "eq?":
                    return BoolLit.getInstance(arg1 == arg2);
                case "procedure?":
                    return BoolLit.getInstance(arg1.isProcedure());
                case "read": //call parser and returns parse tree
                    Parser parse = new Parser(new Scanner(Console.In), new TreeBuilder());
                    return (Node)parse.parseExp();
                case "write":
                    return new StringLit("Cha{" + arg1.ToString() + "/*WRITE*\\}");
                case "display":
                    return new StringLit("Cha{" + arg1.ToString() + "/*DISPLAY*\\}"); //not truly implemented, placeholder code I tink
                case "newline": //\n?
                    return new StringLit("\n");
                case "eval":
                    return arg1.eval(new Environment());
                case "apply":
                    return arg1.apply(arg2);
                case "interaction-environment":
                    //args.env
                    //do
                    //{
                    //    //TODO
                    //    env = new Environment();
                    //} while (env != null);
                    return new Environment();
            }
            return null;
        }

        private Node BinArith(int arg1, int arg2, string s)
        {
            switch (s)
            {
                case "+":
                    return new IntLit(arg1 + arg2);
                case "-":
                    return new IntLit(arg1 - arg2);
                case "*":
                    return new IntLit(arg1 * arg2);
                case "/":
                    return new IntLit(arg1 / arg2);
                case "=":
                    return BoolLit.getInstance(arg1 == arg2);
                case "<":
                    return BoolLit.getInstance(arg1 < arg2);
                case ">":
                    return BoolLit.getInstance(arg1 > arg2);
                default:
                    return null;
            }
        }
    }    
}

