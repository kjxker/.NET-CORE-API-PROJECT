using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marvel_site_api.Model
{
    public class MarvelCharacter
    {
        public virtual string id { get; set; }
        public virtual string name { get; set; }
        public virtual string desc { get; set; }

    }

    public class MarvelCharacters
    {
        public virtual string id { get; set; }
        public virtual string name { get; set; }

        public virtual Thumbnail thumbnail { get; set; }
        public virtual string image
        {
            get { return thumbnail.path + "/"+ thumbnail.extension; }
        }

    }


    public class Thumbnail
    {
        public virtual string path { get; set; }
        public virtual string extension { get; set; }
    }

}
