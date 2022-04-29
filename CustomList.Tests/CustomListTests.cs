using NUnit.Framework;
using FluentAssertions;
using System;
using Telerik.JustMock;


namespace CustomList.Tests
{

    [TestFixture]
    public class CustomListTests
    {

        [Test]
        public void Clear_ClearNotEmptyList_True()
        {
            List<int> list = new() { 4, 6, 1, 6, 5, 7, 9, 11 };
            list.Clear();
            list.Should().BeEmpty();
        }


        [Test]
        public void Add_FireEventWithLambdaInEventArgs_True()
        {
            ListEventArgs args = null;

            var mock = Mock.Create<List<int>>(Constructor.NotMocked);

            Mock.Arrange(() => mock.Add(Arg.IsAny<int>()))
                .Raises(() => mock.Notify += null, (string message) => new ListEventArgs(message) { Message = message });

            mock.Notify += (sender, e) => args = e;

            mock.Add(3);

            args.Message.Should().BeEquivalentTo("Element " + 3 + " added to the list.");

        }

        [Test]
        public void Contains_CheckIfElementOnList_True()
        {
            List<int> list = new() { 4, 6, 1 };
            bool actualResult = list.Contains(4);
            actualResult.Should().BeTrue();
        }


        [Test]
        public void Contains_CheckIfElementOnList_False()
        {
            List<int> list = new() { 4, 6, 1 };
            bool actualResult = list.Contains(29);
            actualResult.Should().BeFalse();
        }


        [Test]
        public void IndexOf_GetIndexOfElement_Return4()
        {
            List<int> list = new() { 4, 6, 1, 6, 5, 7, 9, 11 };
            int actualResult = list.IndexOf(5);
            actualResult.Should().Be(4);

        }


        [Test]
        public void IndexOf_GetIndexOfElement_Return()
        {
            List<int> list = new() { 4, 6, 1, 6, 5, 7, 9, 11 };
            int actualResult = list.IndexOf(29);
            actualResult.Should().Be(-1);
        }


        [TestCase(26, 8)]
        [TestCase(-4, 6)]
        public void Insert_InsertElementOnInvalidIndex_ThrowsArgumentOutOfRangeException(int ind, int val)
        {
            var subject = new List<int> { 4, 6, 4, 1, 6, 5, 7, 9, 11 };
            subject.Invoking(y => y.Insert(ind, val))
                .Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage($"Specified argument was out of the range of valid values. (Parameter '{ind}')");
        }


        [TestCase(0, 5)]
        [TestCase(2, 8)]
        public void Insert_InsertNewElement_NewElementAdded(int a, int b)
        {
            List<int> list = new() { 4, 6, 1 };
            list.Insert(a, b);
            list.Should().HaveElementAt(a, b);
        }


        [Test]
        public void RemoveAt_RemoveElement_ElementRemoved()
        {
            List<int> list = new() { 4, 6, 4, 1, 6, 5, 7, 9, 11 };
            List<int> expectedList = new() { 4, 6, 1, 6, 5, 7, 9, 11};

            list.RemoveAt(2);
            list.Should().Equal(expectedList);

        }


        [Test]
        public void Remove_RemoveElement_True()
        {
            List<int> list = new() { 4, 6, 4, 1, 6, 5, 7, 9, 11 };
            bool actualResult = list.Remove(11);
            actualResult.Should().BeTrue();
        }


        [Test]
        public void Remove_RemoveElement_ArgumentOutOfRangeException()
        {
            List<int> list = new() { 4, 6, 4, 1, 6, 5, 7, 9, 11 };
            list.Invoking(y => y.Remove(34))
                .Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Specified argument was out of the range of valid values. (Parameter '-1')");
        }


        [Test]
        public void CopyTo_CopyListData_DataAdded()
        {
            List<int> list = new() { 42, -1, 4, 10 };
            var arr = new int[7];
 
            list.CopyTo(arr, 2);
            arr.Should().Equal(new[] { 0, 0, 42, -1, 4, 10, 0 });
        }


        [TestCase(5)]
        [TestCase(-10)]
        [TestCase(26)]
        public void CopyTo_CopyListData_ThrowsArgumentOutOfRangeException(int a)
        {
            List<int> list = new() { 46, 23, 20 };
            var arr = new int[4];
            list.Invoking(y => y.CopyTo(arr, a))
                .Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage($"Specified argument was out of the range of valid values. (Parameter '{a}')");

        }


        [Test]
        public void Clear_FireEventWithLambdaInEventArgs_True()
        {
            ListEventArgs args = null;

            var mock = Mock.Create<List<int>>(Constructor.NotMocked);

            Mock.Arrange(() => mock.Clear())
                .Raises(() => mock.Notify += null, (string message) => new ListEventArgs(message) { Message = message });

            mock.Notify += (sender, e) => args = e;

            mock.Clear();

            args.Message.Should().BeEquivalentTo("List is empty");

        }

    }
}