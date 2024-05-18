# ControlSystemPlatform
Sandbox project for playing around with some Smart Warehouse problems.

## Running the API
The solution will create a database and seed items to it. When run it will launch a swagger page.
<br>
There is validation that the order items exists. <br>
If you wanna try the swagger endpoint here is a few SKU numbers you can use:
- SKU0123
- SKU0042
- SKU0101

## Architecture
The solution is created using Mediatr pattern and a classic 3 layer architecture.<br>
A simple Result-pattern is used to make  for the domain validation.<br>

### Concept in-the-making:
Creating a transport order sets a lot of events into motion.<br>
The route should be planned and scheduled. There might be need for restocking and much more.<br>
The idea divide and conquer. <br>
The create TransportOrder, reserves the items, and creates the order. It publishes an event when done, that will start the related work flows.

Note: There is still missing a lot of test, validation and logging. A lot of cases are not handled. The eventbus and related events are also missing.

## Test
So far only a few API test are added

Frameworks:
- FakeItEasy
- FluentAssertion
- AutoData
- EF InMemoryDb & Testserver

