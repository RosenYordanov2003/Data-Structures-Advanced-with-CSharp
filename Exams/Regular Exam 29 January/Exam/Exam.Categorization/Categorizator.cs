namespace Exam.Categorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Categorizator : ICategorizator
    {
        private readonly Dictionary<string, Category> categories;
        public Categorizator()
        {
            categories = new Dictionary<string, Category>();
        }
        public void AddCategory(Category category)
        {
            if (Contains(category))
            {
                throw new ArgumentException();
            }
            categories.Add(category.Id, category);
        }

        public void AssignParent(string childCategoryId, string parentCategoryId)
        {
            if (!categories.ContainsKey(childCategoryId) || !categories.ContainsKey(parentCategoryId))
            {
                throw new ArgumentException();
            }
            Category child = categories[childCategoryId];
            Category parent = categories[parentCategoryId];


            if (child.Parent == parent)
            {
                throw new ArgumentException();
            }
            child.Parent = parent;
            parent.Children.Add(child);

            Category ancestor = parent;

            while (ancestor.Parent != null)
            {
                ancestor = ancestor.Parent;
            }
            CalculateDepth(ancestor);
        }

        private int CalculateDepth(Category node)
        {
            if (node == null)
            {
                return 0;
            }

            int depth = 0;
            foreach (Category category in node.Children)
            {
                depth = Math.Max(CalculateDepth(category), depth);
            }

            node.Depth = depth + 1;

            return node.Depth;
        }

        public bool Contains(Category category)
        {
            return categories.ContainsKey(category.Id);
        }

        public IEnumerable<Category> GetChildren(string categoryId)
        {
            if (!categories.ContainsKey(categoryId))
            {
                throw new ArgumentException();
            }
            Category category = categories[categoryId];
            List<Category> categoriesToReturn = new List<Category>();

            GetChildrenRecurisvely(categoriesToReturn, category);

            return categoriesToReturn;
        }

        private void GetChildrenRecurisvely(List<Category> categoriesToReturn, Category category)
        {
            if (category == null)
            {
                return;
            }
            foreach (Category child in category.Children)
            {
                categoriesToReturn.Add(child);
                GetChildrenRecurisvely(categoriesToReturn, child);
            }
        }

        public IEnumerable<Category> GetHierarchy(string categoryId)
        {
            if (!categories.ContainsKey(categoryId))
            {
                throw new ArgumentException();
            }
            Category category = categories[categoryId];
            Stack<Category> categoryHierarchy = new Stack<Category>();

            GetHierarchy(category, categoryHierarchy);

            return categoryHierarchy;
        }

        private void GetHierarchy(Category category, Stack<Category> categories)
        {
            while (category != null)
            {
                categories.Push(category);
                category = category.Parent;
            }
        }

        public IEnumerable<Category> GetTop3CategoriesOrderedByDepthOfChildrenThenByName()
        {
            return categories.Values.OrderByDescending(c => c.Depth).ThenBy(c => c.Name).Take(3);
        }

        public void RemoveCategory(string categoryId)
        {
            if (!categories.ContainsKey(categoryId))
            {
                throw new ArgumentException();
            }
            Category categoryToDelete = categories[categoryId];
            if (categoryToDelete.Parent != null)
            {
                categoryToDelete.Parent.Children.Remove(categoryToDelete);
                categoryToDelete.Parent = null;
            }
            categories.Remove(categoryId);
            Delete(categoryToDelete);

        }
        private void Delete(Category root)
        {
            if (root == null)
            {
                return;
            }
            foreach (Category category in root.Children)
            {
                Delete(category);
                categories.Remove(category.Id);
            }
        }

        public int Size() => categories.Count;

    }
}
