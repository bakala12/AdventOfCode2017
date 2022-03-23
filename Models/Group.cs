using System.Text;

namespace AdventOfCode2017.Models
{
    public abstract class Element 
    {
        public abstract long CountCharacters();
    }

    public class Group : Element
    {
        public List<Element> Elements { get; }

        public Group(List<Element> elements)
        {
            Elements = elements;
        }

        public long GetScore(int depth = 1) 
        {
            return Elements.OfType<Group>().Sum(e => e.GetScore(depth+1)) + depth;
        }

        public override long CountCharacters()
        {
            return Elements.Sum(e => e.CountCharacters());
        }
    }

    public class StringElement : Element
    {
        public string Content { get; }

        public StringElement(string content)
        {
            Content = content;
        }

        public override long CountCharacters()
        {
            return 0;
        }
    }

    public class GarbageElement : Element
    {
        public string Content { get; }

        public GarbageElement(string content)
        {
            Content = content;
        }

        public override long CountCharacters()
        {
            bool ignored = false;
            int c = 0;
            for(int i = 0; i < Content.Length; i++)
            {
                if(Content[i] != '!')
                {
                    if(!ignored) 
                        c++;
                    ignored = false;
                }
                else
                {
                    ignored = !ignored;
                }
            }
            return c;
        }
    }

    public static class Parser 
    {
        public  static Group ParseGroup(Queue<char> input)
        {
            var elements = new List<Element>();
            input.Dequeue(); //'{'
            char c;
            while((c = input.Peek()) != '}')
            {
                if(c == '{')
                    elements.Add(ParseGroup(input));
                else if(c == '<')
                    elements.Add(ParseGarbage(input));
                else
                    elements.Add(ParseStringElement(input));
                if(input.Peek() == ',')
                    input.Dequeue();
            }
            input.Dequeue();
            return new Group(elements);
        }

        private static StringElement ParseStringElement(Queue<char> input)
        {
            var sb = new StringBuilder();
            char c;
            while((c = input.Peek()) != ',' && c != '}')
                sb.Append(input.Dequeue());           
            return new StringElement(sb.ToString());
        }

        private static GarbageElement ParseGarbage(Queue<char> input)
        {
            var sb = new StringBuilder();
            input.Dequeue(); //'<'
            bool lastIgnore = false;
            char c;
            while((c = input.Peek()) != '>' || lastIgnore)
            {
                input.Dequeue(); // c
                sb.Append(c);
                lastIgnore = !(c != '!' || lastIgnore);
            }
            input.Dequeue(); //'>'
            return new GarbageElement(sb.ToString());
        }
    }
}