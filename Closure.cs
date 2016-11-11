// Closure.java -- the data structure for function closures

// Class Closure is used to represent the value of lambda expressions.
// It consists of the lambda expression itself, together with the
// environment in which the lambda expression was evaluated.

// The method apply() takes the environment out of the closure,
// adds a new frame for the function call, defines bindings for the
// parameters with the argument values in the new frame, and evaluates
// the function body.

using System;

namespace Tree
{
    public class Closure : Node
    {
        private Node fun;		// a lambda expression
        private Environment env;	// the environment in which
                                        // the function was defined

        public Closure(Node f, Environment e)	{ fun = f;  env = e; }

        public Node getFun()		{ return fun; }
        public Environment getEnv()	{ return env; }

        // TODO: The method isProcedure() should be defined in
        // class Node to return false.
        public override bool isProcedure()	{ return true; }

        public override void print(int n) {
            // there got to be a more efficient way to print n spaces
            for (int i = 0; i < n; i++)
                Console.Write(' ');
            Console.WriteLine("#{Procedure");
            if (fun != null)
                fun.print(Math.Abs(n) + 4);
            for (int i = 0; i < Math.Abs(n); i++)
                Console.Write(' ');
            Console.WriteLine('}');
        }

        // TODO: The method apply() should be defined in class Node
        // to report an error.  It should be overridden only in classes
        // BuiltIn and Closure.
        /*
         * Before calling the function apply, its arguments are evaluated
         * the first argument must be a closure, then apply needs todo
         * extract the environment out of the closure
         * add a new frame to the environment that binds the parameters
         * to the corresponding argument values
         * recursively call eval for the function body and the new enviornment
         * 
         */
        public override Node apply (Node args)
        {
            //need to get both args
            Node arg1 = args.getCar(), arg2 = args.getCdr();

            //need to see if arg1 is a Closure
            if(arg1.isProcedure())
            {
                //getting the environment out of the Closure arg1
                Environment newEnv = ((Closure)arg1).getEnv();
                //adding a new frame to the environment
                newEnv.define(((Closure)arg1).getFun(), arg2);
                //trying to recursively call eval 
                newEnv.eval(newEnv);
                //return environment?
                return newEnv;
            }
            else
            {
                return new StringLit("Error: apply on non closure");
            }
        }
    }    
}
