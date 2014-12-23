using System;

namespace BPDMH.Model
{
    public abstract class RepoClass
    {
        public abstract Repo CreateRepo(string repoName, string[] repoCol, string sqlSyntax);

//        public Repo OrderPizza(string repoName, string[] repoCol, string sqlSyntax) 
//        {
//		    Repo repo = CreateRepo(string repoName, string[] repoCol, string sqlSyntax);
//		
//		    repo.Prepare();
//		    repo.Bake();
//		    repo.Cut();
//		    repo.Box();
//		    return repo;
//	    }
    }

    public class Repo
    {
        public void Prepare()
        {
            throw new NotImplementedException();
        }

        public void Bake()
        {
            throw new NotImplementedException();
        }

        public void Cut()
        {

        }

        public void Box()
        {
            throw new NotImplementedException();
        }
    }
}