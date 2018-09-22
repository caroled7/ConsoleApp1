using System;
using System.Collections.Generic;
using System.Text;


namespace Assignment_2
{
  public class StockList
  {
    private StockNode head;

    //Constructor for initialization
    public StockList()
    {
      this.head = null;
    }

    //param        : NA
    //summary      : checks if the list is empty
    //return       : true if list is empty, false otherwise
    //return type  : bool
    public bool IsEmpty()
    {
      if (this.head == null)
      {
        return true;
      }
      return false;
    }

    //param (Stock)stock : stock that is to be added
    //summary      : Add node at first position in list
    //                This is done by creating a new node 
    //                  and pointing it to the current list 
    //return       : NA
    //return type  : NA
    public void AddFirst(Stock stock)
    {
      StockNode nodeToAdd = new StockNode(stock);
      nodeToAdd.Next = head;
      head = nodeToAdd;
    }

    //param (Stock)stock : stock that is to be added
    //summary      : Add mode at last position of list
    //  This is done by traversing the list till we reach the end
    // and pointing the last node to the new node
    // return       :
    // return type  :
    public void AddLast(Stock stock)
    {
      // for an empty list, we add the node at the top of the list
      if (this.IsEmpty())
        AddFirst(stock);
      else
      {
        // traverse the list till the end
        StockNode current = this.head;
        while (current.Next != null)
          current = current.Next;
        

        // point the last node to the new node
        StockNode nodeToAdd = new StockNode(stock);
        current.Next = nodeToAdd;
      }
    }

    /// <summary>
    /// Add node in an alphabetically sorted manner, if stock is already present then set holdings
    /// to sum of existing and new stock
    ///   We assume that the list is always sorted in alphabetical order
    ///   The stock may be added either at:
    ///     the top of the list (if alphabetically lower than all nodes)
    ///   , middle of the list, in which case, we can either
    ///     Add to existing holdings (if the stock already exists in the list), or
    ///     Insert it at the right location in alphatecial order (if it does not already exist)
    ///   , or end of the list (if alphabetically greater than all existing nodes)
    /// </summary>
    /// <param name="stock">stock that is to be added</param>
    public void AddStock(Stock stock)
    {
      // for an empty list, we add the node at the top of the list
      if (this.IsEmpty())
        AddFirst(stock);
      else
      {
        // if the new node is alphabetically the first, again, we add it at the top of the list
        string nameOfStockToAdd = stock.Name;
        string headNodeData = (this.head.StockHolding).Name;
        if (headNodeData.CompareTo(nameOfStockToAdd) > 0)
          AddFirst(stock);
        else
        {
          // traverse the list to locate the stock
          StockNode current = this.head;
          StockNode previous = null;
          string currentStockName = (current.StockHolding).Name;

          while (current.Next != null && currentStockName.CompareTo(nameOfStockToAdd) < 0)
          {
            previous = current;
            current = current.Next;
            currentStockName = (current.StockHolding).Name;
          }

          // we have now traversed all stocks that are alphabetically less than the stock to be added
          if (current.Next != null)
          {
            // if the stock already exists, add to holdings
            if (currentStockName.CompareTo(nameOfStockToAdd) == 0)
            {
              decimal holdings = (current.StockHolding).Holdings + stock.Holdings;
              current.StockHolding.Holdings = holdings;
            }
            else if (currentStockName.CompareTo(nameOfStockToAdd) > 0)
            {
              // insert the stock in the current position. This requires creating a new node,
              //  pointing the new node to the next node
              //    and pointing the previous node to the current node
              //  QUESTION: what would happen if we flipped the sequence of assignments below?
              StockNode newNode = new StockNode(stock);
              newNode.Next = current;
              previous.Next = newNode;
            }
          }
          else
          {
            // we are at the end of the list, add the stock at the end
            //  This is probably not the most efficient way to do it,
            //  since AddLast traverses the list all over again
            AddLast(stock);
          }
        }
      }
    }

    //param  (Stock)stock : stock that is to be checked 
    //summary      : checks if list contains stock passed as parameter
    //                  This involves traversing the list until we find the stock
    //                    return null if we don't
    //return       : Reference of node with matching stock
    //return type  : StockNode if exists, null if not
    public StockNode Contains(Stock stock)
    {
      StockNode nodeReference = null;

      // if the list is empty, return null
      if (this.IsEmpty())
        return nodeReference;
      else
      {
        // traverse the list until we locate the stock,
        //  or, reach the end of the list
        StockNode current = this.head;
        StockNode previous = this.head;
        while (current.Next != null)
        {
          Stock currentStock = current.StockHolding;

          // found it! Return the node
          if (currentStock.Equals(stock))
          {
            nodeReference = previous;
            break;
          }

          // else, continue traversing
          previous = current;
          current = current.Next;
        }
      }

      return nodeReference;
    }

    /// <summary>
    /// swaps the node passed as argument with next node in list
    /// Sorting the list using the simple bubble sort algorithm requires repeatdely traversing
    ///   the list and pushing a node down the list until it falls in place
    ///     Pushing the node is essentially a swap operation, where we take the next node
    ///       and put it in the current position and move the current node to the next position on the list
    /// </summary>
    /// <param name="nodeOne">first node to be swapped</param>
    /// <returns>Reference to current node</returns>
    public StockNode Swap(Stock nodeOne)
    {
      StockNode prevNodeOne = null;
      StockNode currNodeOne = this.head;

      // traverse the list until we reach the node to swap
      while (currNodeOne != null && currNodeOne.StockHolding != nodeOne)
      {
        prevNodeOne = currNodeOne;
        currNodeOne = currNodeOne.Next;
      }

      // maintain references to the nodes to be swapped
      StockNode prevNodeTwo = currNodeOne;
      StockNode currNodeTwo = currNodeOne.Next;

      // handle corner cases, maybe we have reached the end of the list
      if (currNodeOne == null || currNodeTwo == null)
        return null;

      // perhaps the insertion is at the top of the list
      if (prevNodeOne != null)
        prevNodeOne.Next = currNodeTwo;
      else
        this.head = currNodeTwo;

      if (prevNodeTwo != null)
        prevNodeTwo.Next = currNodeOne;
      else
        this.head = currNodeOne;

      // normal case, swap nodes
      StockNode temp = currNodeOne.Next;
      currNodeOne.Next = currNodeTwo.Next;
      currNodeTwo.Next = temp;

      return currNodeTwo;
    }

    
    // FOR STUDENTS

 // implement a utilitiy function list count to track number of stocks in stock list.

  public int Count()
        {    int counter = 0;


             if (this.IsEmpty() == true)
              {
                counter =  0;
              }
           else
              {
                StockNode current = this.head;

                while (current.Next != null)
                {
                    current = current.Next;
                    counter = counter + 1;
                }
               }

              return counter;
        }




        //param        : NA
        //summary      : Sort the list by descending number of holdings
        //return       : NA
        //return type  : NA
        public void SortByValue() // Carole
        {

            // if there is nothing in the list, stop.
            if (this.IsEmpty())
            {
                return;
            }
            // do a double loop to compare every item in the list
            // with every other item in the list 
            // based on bubble sort implementation of DSA guide - c sharp code download
            // grab first stock in stock list and next stock in list
            // compare their number of stocks
            // if first stock greater than second stock, leave first stock in its place
            // if first stock less than second stock, swap stocks
            else
            {
                StockNode nodeReference = null;

                StockNode currentNode = this.head;
                StockNode nextNode = currentNode.Next;

                decimal currNumHoldings = currentNode.StockHolding.Holdings;
                decimal nextNumHoldings = nextNode.StockHolding.Holdings;

                for (int i = 0; i < this.Count(); i++)
                {
                    for (int j = 0; j < this.Count(); j++)
                    {

                        if (currNumHoldings < nextNumHoldings)  // swap
                        {
                            nodeReference = this.Swap(currentNode.StockHolding);
                            currentNode = nodeReference;
                            currNumHoldings = currentNode.StockHolding.Holdings;
                            nextNode = currentNode.Next;
                            nextNumHoldings = nextNode.StockHolding.Holdings;
                        }
                        else // no need to swap, just move on to next node
                        {
                            currentNode = nextNode;
                            currNumHoldings = currentNode.StockHolding.Holdings;
                            nextNode = currentNode.Next;
                            nextNumHoldings = nextNode.StockHolding.Holdings;
                            nodeReference = null; // not sure if I need this 

                        }

                    }
                }



            }

        }

    //param        : NA
    //summary      : Sort the list alphabatically
    //return       : NA
    //return type  : NA
    public void SortByName()

            { 


  // if there is nothing in the list, stop.
            if (this.IsEmpty())
            {
                return;
            }
            // do a double loop to compare every item in the list
            // with every other item in the list 
            // based on bubble sort implementation of DSA guide - c sharp code download
            // grab first stock in stock list and next stock in list
            // compare their names
            // if first stock name greater than second stock, leave first stock in its place
            // if first stock name less than second stock name, swap stocks
            else
            {
                StockNode nodeReference = null;

                StockNode currentNode = this.head;
                StockNode nextNode = currentNode.Next;

                String currStockName = currentNode.StockHoldings.Name;
                String nextStockName = nextNode.StockHoldings.Name;

    
                for (int i = 0; i < this.Count(); i++)
                {
                    for (int j = 0; j < this.Count(); j++)
                    {
                        // String.Compare (strA, str B)
                        //  Less than zero	strA precedes strB in the sort order.
                        // Zero strA occurs in the same position as strB in the sort order.
                        //Greater than zero strA follows strB in the sort order.
                        // String.Compare (currStockName, nextStockName) < 0  
                        // already in right sort order, move on to next node
                        // if  String.Compare (currStockName, nextStockName) < 0  
                        // not in right sort order, need to swap




                        if ( String.Compare (currStockName, nextStockName) > 0)  // swap
                            {
                            nodeReference = this.Swap(currentNode.StockHolding);
                            currentNode = nodeReference;
                            currStockName = currentNode.StockHoldings.Name;
                            nextNode = currentNode.Next;
                            nextStockName = nextNode.StockHoldings.Name;


                            String currStockName = currentNode.StockHoldings.Name;
                            String nextStockName = nextNode.StockHoldings.Name;

                        }
                        else // no need to swap, just move on to next node
                        {
                            currentNode = nextNode;
                            currStockName = currentNode.StockHoldings.Name;
                            nextNode = currentNode.Next;
                            nextStockName = nextNode.StockHoldings.Name;
                            nodeReference = null; // not sure if I need this 

                        }

                    }
                }



            }

            //param   (StockList)listToMerge : second list to be merged 
            //summary      : merge two different list into a single result list
            //return       : merged list
            //return type  : StockList
     

            // use AddStock function and Count functions as helpers
            // 1. Sort lists by name
            // 2. determine count of elements in each list
            // 3.  Pick as main list the larger list
            // 4.  Loop through the other list and add stock using add stock function
            // 5.  return new list
            

            public StockList MergeList(StockList listToMerge) 
    {

                // write your implementation here
                StockList resultList = new StockList();
                resultlist = this; // set resultlist to current list
                resultlist.SortbyName();

                StockNode currentNode = listToMerge.head;
                

                
                for ( int i = 0; i < listToMerge.Count()  ) //loop through list to merge
                {
                    this.AddStock(currentNode.Stock);
                    currentNode = currentNode.Next;
                }
                

                 return resultList;
    }

    //param        : NA
    //summary      : finds the stock with most number of holdings
    //return       : stock with most shares
    //return type  : Stock
    public Stock MostShares()
    {
      Stock mostShareStock = null;
    -- if do Sort by value first, then it is a matter of getting the first item in list - 
        -- should compare to for other cases
      // write your implementation here

      return mostShareStock;
    }

    //param        : NA
    //summary      : finds the number of nodes present in the list
    //return       : length of list
    //return type  : int
    public int Length()
    {
      int length = 0;

      // write your implementation here
      -- .next = null -- do a loop until you get to end  -- do while

      return length;
    }

    //param        : NA
    //summary      : Calculate the value of each node by multiplying holdings with price, and returns the total value
    //return       : total value
    //return type  : decimal
    public decimal Value()
    {
      decimal value = 0.0m;

      // write your implementation here
      -- do a  loop until .next is null
        -- do multiplication

      return value;
    }

    //param  (StockList) listToCompare     : StockList which has to comared for similarity index
    //summary      : finds the similar number of nodes between two lists
    //return       : similarty index
    //return type  : int
    public int Similarity(StockList listToCompare)
    {
      int similarityIndex = 0;

      // write your implementation here
      -- two loops - assumes stocks are sorted
        -- look at compare algorithm in algorithm examples

      return similarityIndex;
    }

    //param        : NA
    //summary      : Print all the nodes present in the list
    //return       : NA
    //return type  : NA
    public void Print()
    {
      // write your implementation here

    }
  }
}
